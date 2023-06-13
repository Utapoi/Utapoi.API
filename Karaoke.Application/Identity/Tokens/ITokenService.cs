using FluentResults;
using Karaoke.Application.Auth.Commands.GetRefreshToken;
using Karaoke.Application.Auth.Responses;

namespace Karaoke.Application.Identity.Tokens;

public interface ITokenService
{
    Task<Result<TokenResponse>> GetTokenAsync(
        string loginProvider,
        string providerKey,
        string ipAddress,
        CancellationToken cancellationToken = default
    );

    Task<Result<TokenResponse>> GetRefreshTokenAsync(GetRefreshToken.Command request);
}