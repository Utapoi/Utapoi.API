using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Karaoke.Application.Auth.Commands.RefreshToken;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Application.Identity.Extensions;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Infrastructure.Identity.Entities;
using Karaoke.Infrastructure.Options.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Karaoke.Infrastructure.Identity.Tokens;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions
    )
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshToken.Command request)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        var userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail!);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new ForbiddenAccessException();
        }

        return await GenerateTokensAndUpdateUser(user);
    }

    public async Task<Result<TokenResponse>> GetTokenAsync(string loginProvider, string providerKey)
    {
        var user = await _userManager.FindByLoginAsync(loginProvider, providerKey);

        if (user == null)
        {
            return Result.Fail("User not found");
        }

        var token = await GenerateTokensAndUpdateUser(user);
        token.TokenSource = GetTokenSource(loginProvider);

        return Result.Ok(token);
    }

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user)
    {
        var token = await GenerateJwt(user);

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays);

        await _userManager.UpdateAsync(user);

        return new TokenResponse
        {
            Token = token,
            RefreshToken = user.RefreshToken,
            TokenExpiryTime = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes),
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
        };
    }

    private async Task<string> GenerateJwt(ApplicationUser user)
    {
        return GenerateEncryptedToken(GetSigningCredentials(), await GetClaims(user));
    }

    private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Role, string.Join(',', roles))
        };
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
}