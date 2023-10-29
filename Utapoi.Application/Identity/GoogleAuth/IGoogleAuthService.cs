using FluentResults;
using Utapoi.Application.Auth.GoogleAuth.Requests.LoginRequest;
using Utapoi.Application.Auth.Responses;

namespace Utapoi.Application.Identity.GoogleAuth;

public interface IGoogleAuthService
{
    GoogleAuthProperties GetAuthorizeUrl();

    Task<Result<TokenResponse>> LoginAsync(GoogleLogin.Request request, CancellationToken cancellationToken = default);
}