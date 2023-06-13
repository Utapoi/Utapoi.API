using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Karaoke.Application.Auth.Commands.GetRefreshToken;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Application.Identity.Extensions;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Infrastructure.Identity.Entities;
using Karaoke.Infrastructure.Options.JWT;
using Karaoke.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Karaoke.Infrastructure.Identity.Tokens;

public class TokenService : ITokenService
{
    private readonly AuthDbContext _context;

    private readonly JwtOptions _jwtOptions;

    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions,
        AuthDbContext context
    )
    {
        _userManager = userManager;
        _context = context;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<TokenResponse>> GetTokenAsync(
        string loginProvider,
        string providerKey,
        string ipAddress,
        CancellationToken cancellationToken = default
    )
    {
        var user = await _userManager.FindByLoginAsync(loginProvider, providerKey);

        if (user == null)
        {
            return Result.Fail("User not found");
        }

        if (TryGetValidTokenForUser(user, ipAddress, out var token) &&
            TryGetValidRefreshTokenForUser(user, ipAddress, out var refreshToken))
        {
            return Result.Ok(new TokenResponse
            {
                Token = token.AccessToken,
                RefreshToken = refreshToken!.Token,
                TokenExpiryTime = token.ExpiresAt,
                RefreshTokenExpiryTime = refreshToken!.ExpiresAt
            });
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<Result<TokenResponse>> GetRefreshTokenAsync(GetRefreshToken.Command request)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        var userEmail = userPrincipal.GetEmail();
        var user = await _context.Users
            .Include(x => x.Tokens)
            .Include(x => x.RefreshTokens)
            .ThenInclude(r => r.AccessToken)
            .FirstOrDefaultAsync(
                x => x.Email == userEmail
                     || x.NormalizedEmail == userEmail
            );

        if (user is null)
        {
            return Result.Fail("User not found");
        }

        var refreshToken = user.RefreshTokens
            .SingleOrDefault(
                x => x.Token == request.RefreshToken
                     && x.IpAddress == request.IpAddress
            );

        var token = user.Tokens
            .SingleOrDefault(
                x => x.AccessToken == request.Token
                     && x.IpAddress == request.IpAddress
            );

        if (refreshToken is null || token is null)
        {
            return Result.Fail("Invalid token");
        }

        if (refreshToken.AccessToken.AccessToken != token.AccessToken)
        {
            // Be sure that the refresh token and the access token are related.
            // If they are not, we delete them both.
            await RemoveTokens(token, refreshToken, user);

            return Result.Fail("Invalid token");
        }

        if (refreshToken.IsExpired)
        {
            // If the refresh token is expired, we should delete it and the associated access token.
            await RemoveTokens(token, refreshToken, user);

            return Result.Fail("Refresh token has expired");
        }

        if (!refreshToken.IsValid)
        {
            // If the refresh token is invalid, we should delete it and the associated access token.
            // This could be because the user has his credentials stolen.
            // We maybe should also delete all tokens that were created after this one.
            await RemoveTokens(token, refreshToken, user);

            return Result.Fail("Refresh token is invalid");
        }

        // At this point, the refresh token should be valid.
        // We can remove the old tokens and generate a new one.
        await RemoveTokens(token, refreshToken, user);

        return await GenerateTokensAndUpdateUser(user, request.IpAddress);
    }

    private bool TryGetValidTokenForUser(
        ApplicationUser user,
        string ipAddress,
        [MaybeNullWhen(false)] out Token token
    )
    {
        token = _context.Tokens
            .FirstOrDefault(
                x => x.UserId == user.Id
                     && x.IpAddress == ipAddress
                     && x.ExpiresAt >= DateTime.UtcNow
            );

        return token != null;
    }

    private bool TryGetValidRefreshTokenForUser(
        ApplicationUser user,
        string ipAddress,
        [MaybeNullWhen(false)] out RefreshToken refreshToken
    )
    {
        refreshToken = _context.RefreshTokens
            .Include(x => x.AccessToken)
            .FirstOrDefault(
                x => x.UserId == user.Id
                     && x.IpAddress == ipAddress
                     && x.ExpiresAt >= DateTime.UtcNow
                     && x.UsageCount == 0
            );

        return refreshToken != null;
    }

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress)
    {
        var token = _context.Tokens.Add(new Token
        {
            AccessToken = await GenerateJwt(user),
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes),
            IpAddress = ipAddress,
            User = user,
            UserId = user.Id
        }).Entity;

        var refreshToken = _context.RefreshTokens.Add(new RefreshToken
        {
            AccessToken = token,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays),
            IpAddress = ipAddress,
            User = user,
            UserId = user.Id
        }).Entity;

        await _context.SaveChangesAsync();

        user.Tokens.Add(token);
        user.RefreshTokens.Add(refreshToken);

        return new TokenResponse
        {
            Token = token.AccessToken,
            RefreshToken = refreshToken.Token,
            TokenExpiryTime = token.ExpiresAt,
            RefreshTokenExpiryTime = refreshToken.ExpiresAt
        };
    }

    private async Task<string> GenerateJwt(ApplicationUser user)
    {
        return GenerateEncryptedToken(GetSigningCredentials(), await GetClaims(user));
    }

    private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private static TokenSource GetTokenSource(string loginProvider)
    {
        return loginProvider switch
        {
            "Google" => TokenSource.Google,
            "Discord" => TokenSource.Discord,
            _ => TokenSource.None
        };
    }

    private static string GenerateRefreshToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var randomNumber = new byte[32];

        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes),
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(SHA512.HashData(Encoding.UTF8.GetBytes(_jwtOptions.Key))),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = _jwtOptions.ValidAudience,
            ValidIssuer = _jwtOptions.ValidIssuer,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ForbiddenAccessException();
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = SHA512.HashData(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }

    private async Task RemoveTokens(Token token, RefreshToken refreshToken, ApplicationUser user)
    {
        user.RefreshTokens.Remove(refreshToken);
        user.Tokens.Remove(token);

        await _context.SaveChangesAsync();
    }
}