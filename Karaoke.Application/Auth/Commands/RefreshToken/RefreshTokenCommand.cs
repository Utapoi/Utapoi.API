using FluentValidation;
using JetBrains.Annotations;
using Karaoke.Application.Identity.Tokens;
using MediatR;

namespace Karaoke.Application.Auth.Commands.RefreshToken;

public static class RefreshToken
{
    public sealed class Command : IRequest<Response>
    {
        public string Token { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public string IpAddress { get; init; } = string.Empty;
    }

    public sealed class Response
    {
        public string Token { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public DateTime RefreshTokenExpiryTime { get; init; }
    }

    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x => x.IpAddress).NotEmpty();
        }
    }

    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Command, Response>
    {
        private readonly ITokenService _tokenService;

        public Handler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            return _tokenService.RefreshTokenAsync(request);
        }
    }
}