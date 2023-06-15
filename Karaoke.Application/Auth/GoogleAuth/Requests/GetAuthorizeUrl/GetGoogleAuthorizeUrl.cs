using FluentResults;
using JetBrains.Annotations;
using Karaoke.Application.Identity.GoogleAuth;
using MediatR;

namespace Karaoke.Application.Auth.GoogleAuth.Requests.GetAuthorizeUrl;

/// <summary>
///     Represents a request for getting the Google authorization URL.
/// </summary>
public static class GetGoogleAuthorizeUrl
{
    /// <summary>
    ///     The request class for the <see cref="GetGoogleAuthorizeUrl" /> request.
    /// </summary>
    public sealed class Request : IRequest<Result<GoogleAuthProperties>>
    {
    }

    /// <summary>
    ///     The handler class for the <see cref="Request" />.
    /// </summary>
    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Request, Result<GoogleAuthProperties>>
    {
        private readonly IGoogleAuthService _googleAuthService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="googleAuthService">
        ///     The Google authentication service.
        /// </param>
        public Handler(IGoogleAuthService googleAuthService)
        {
            _googleAuthService = googleAuthService;
        }

        /// <summary>
        ///     Handles the <see cref="Request" />.
        /// </summary>
        /// <param name="request">
        ///     The request.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token.
        /// </param>
        /// <returns>
        ///     A <see cref="Result" /> containing the Google authorization URL.
        /// </returns>
        public Task<Result<GoogleAuthProperties>> Handle(Request request, CancellationToken cancellationToken)
        {
            var url = _googleAuthService.GetAuthorizeUrl();

            return Task.FromResult(Result.Ok(url));
        }
    }
}