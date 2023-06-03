using FluentResults;
using Karaoke.Application.Auth.Responses;
using Microsoft.AspNetCore.Authentication;

namespace Karaoke.Application.Identity.GoogleAuth;

public interface IGoogleAuthService
{
    AuthenticationProperties GetAuthorizeUrl();

    Task<Result<TokenResponse>> LoginAsync(CancellationToken cancellationToken = default);
}