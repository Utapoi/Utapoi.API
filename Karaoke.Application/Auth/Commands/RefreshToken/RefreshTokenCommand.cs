using FluentValidation;
using JetBrains.Annotations;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Identity.Tokens;
using MediatR;

namespace Karaoke.Application.Auth.Commands.RefreshToken;

public static class RefreshToken
{
    public sealed class Command : IRequest<TokenResponse>
    {
        public string Token { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public string IpAddress { get; init; } = string.Empty;
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
    internal sealed class Handler : IRequestHandler<Command, TokenResponse>
    {
        private readonly ITokenService _tokenService;

        public Handler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task<TokenResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            return _tokenService.RefreshTokenAsync(request);
        }
    }
}