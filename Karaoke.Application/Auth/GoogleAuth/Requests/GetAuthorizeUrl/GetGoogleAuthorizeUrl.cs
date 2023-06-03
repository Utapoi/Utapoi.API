using FluentResults;
using Karaoke.Application.Identity.GoogleAuth;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Karaoke.Application.Auth.GoogleAuth.Requests.GetAuthorizeUrl;

public static class GetGoogleAuthorizeUrl
{
    public record Request : IRequest<Result<AuthenticationProperties>>;

    internal sealed class Handler : IRequestHandler<Request, Result<AuthenticationProperties>>
    {
        private readonly IGoogleAuthService _googleAuthService;

        public Handler(IGoogleAuthService googleAuthService)
        {
            _googleAuthService = googleAuthService;
        }

        public Task<Result<AuthenticationProperties>> Handle(Request request, CancellationToken cancellationToken)
        {
            var url = _googleAuthService.GetAuthorizeUrl();

            return Task.FromResult(Result.Ok(url));
        }
    }
}