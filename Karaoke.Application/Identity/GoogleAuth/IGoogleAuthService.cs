using FluentResults;
using Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;
using Karaoke.Application.Auth.Responses;

namespace Karaoke.Application.Identity.GoogleAuth;

public interface IGoogleAuthService
{
    GoogleAuthProperties GetAuthorizeUrl();

    Task<Result<TokenResponse>> LoginAsync(GoogleLogin.Request request, CancellationToken cancellationToken = default);
}