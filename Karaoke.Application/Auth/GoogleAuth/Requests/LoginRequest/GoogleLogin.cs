using FluentResults;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Identity.GoogleAuth;
using MediatR;

namespace Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;

public static class GoogleLogin
{
    public record Request : IRequest<Result<TokenResponse>>;

    internal sealed class Handler : IRequestHandler<Request, Result<TokenResponse>>
    {
        private readonly IGoogleAuthService _googleAuthService;

        public Handler(IGoogleAuthService googleAuthService)
        {
            _googleAuthService = googleAuthService;
        }

        public Task<Result<TokenResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            return _googleAuthService.LoginAsync(cancellationToken);
        }
    }
}