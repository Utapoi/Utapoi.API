using FluentResults;
using Karaoke.Application.Auth.Commands.RefreshToken;
using Karaoke.Application.Auth.Responses;

namespace Karaoke.Application.Identity.Tokens;

public interface ITokenService
{
    Task<Result<TokenResponse>> GetTokenAsync(string loginProvider, string providerKey);

    Task<TokenResponse> RefreshTokenAsync(RefreshToken.Command request);
}