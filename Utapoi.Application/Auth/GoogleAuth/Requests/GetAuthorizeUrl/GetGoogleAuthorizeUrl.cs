using FluentResults;
using JetBrains.Annotations;
using MediatR;
using Utapoi.Application.Identity.GoogleAuth;

namespace Utapoi.Application.Auth.GoogleAuth.Requests.GetAuthorizeUrl;

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
        public async Task<Result<GoogleAuthProperties>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Result.Fail("Not implemented yet.");
        }
    }
}