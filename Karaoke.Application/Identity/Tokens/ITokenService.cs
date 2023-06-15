using FluentResults;
using Karaoke.Application.Auth.Commands.GetRefreshToken;
using Karaoke.Application.Auth.Responses;

namespace Karaoke.Application.Identity.Tokens;

/// <summary>
///     The token service.
/// </summary>
public interface ITokenService
{
    /// <summary>
    ///     Gets the token for the specified login provider and provider key.
    /// </summary>
    /// <param name="loginProvider">The login provider.</param>
    /// <param name="providerKey">The provider key.</param>
    /// <param name="ipAddress">The ip address of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result" /> containing the token response.
    /// </returns>
    Task<Result<TokenResponse>> GetTokenAsync(
        string loginProvider,
        string providerKey,
        string ipAddress,
        CancellationToken cancellationToken = default
    );

    // TODO: Also rename this method in accordance with the new name of the command.

    /// <summary>
    ///     Gets a new token for the user.
    /// </summary>
    /// <param name="request">The request for obtaining a new token.</param>
    /// <returns>
    ///     A <see cref="Result" /> containing the token response.
    /// </returns>
    Task<Result<TokenResponse>> GetRefreshTokenAsync(GetRefreshToken.Command request);
}