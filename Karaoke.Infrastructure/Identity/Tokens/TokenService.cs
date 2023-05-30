using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Karaoke.Application.Auth.Commands.GetToken;
using Karaoke.Application.Auth.Commands.RefreshToken;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Application.Identity.Extensions;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Infrastructure.Identity.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Karaoke.Infrastructure.Identity.Tokens;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings
    )
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<GetToken.Response> GetTokenAsync(GetToken.Command request)
    {
        if (await _userManager.FindByNameAsync(request.Username.Trim().Normalize()) is not { } user
            || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ForbiddenAccessException();
        }

        return await GenerateTokensAndUpdateUser(user, request.IpAddress);
    }

    public async Task<RefreshToken.Response> RefreshTokenAsync(RefreshToken.Command request)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        var userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail!);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new ForbiddenAccessException();
        }

        var r = await GenerateTokensAndUpdateUser(user, request.IpAddress);

        return new RefreshToken.Response
        {
            Token = r.Token,
            RefreshToken = r.RefreshToken,
            RefreshTokenExpiryTime = r.RefreshTokenExpiryTime
        };
    }

    private async Task<GetToken.Response> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress)
    {
        var token = GenerateJwt(user, ipAddress);

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

        await _userManager.UpdateAsync(user);

        return new GetToken.Response
        {
            Token = token,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
        };
    }

    private string GenerateJwt(ApplicationUser user, string ipAddress)
    {
        return GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));
    }

    private static IEnumerable<Claim> GetClaims(ApplicationUser user, string ipAddress)
    {
        return new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new("IpAddress", ipAddress)
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
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(SHA512.HashData(Encoding.UTF8.GetBytes(_jwtSettings.Key))),
            ValidateIssuer = false,
            ValidateAudience = false,
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
        var key = SHA512.HashData(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }
}