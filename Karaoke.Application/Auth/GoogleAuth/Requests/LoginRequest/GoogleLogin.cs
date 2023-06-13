using FluentResults;
using FluentValidation;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Identity.GoogleAuth;
using MediatR;

namespace Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;

/// <summary>
///     The request to login with Google.
/// </summary>
public static class GoogleLogin
{
    /// <summary>
    ///     The request with information about the client.
    /// </summary>
    public class Request : IRequest<Result<TokenResponse>>
    {
        public Request()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        /// <param name="ipAddress">
        ///     The IP address of the client.
        /// </param>
        public Request(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public string IpAddress { get; set; } = string.Empty;
    }

    /// <summary>
    ///     Validates the inputs passed to the <see cref="Request" />
    /// </summary>
    internal sealed class Validator : AbstractValidator<Request>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Validator" /> class.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.IpAddress).NotEmpty();
        }
    }

    /// <summary>
    ///     The handler for the <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<TokenResponse>>
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

        /// <inheritdoc />
        public Task<Result<TokenResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            return _googleAuthService.LoginAsync(request, cancellationToken);
        }
    }
}