using FluentResults;
using FluentValidation;
using JetBrains.Annotations;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Identity.Tokens;
using MediatR;

namespace Karaoke.Application.Auth.Commands.GetRefreshToken;

// TODO: Rename this class to something more meaningful.
// We don't actually get a refresh token here, we get a new token.
// So maybe something like GetNewToken.Command / UpdateToken.Command / RegenerateToken.Command

public static class GetRefreshToken
{
    public sealed class Command : IRequest<Result<TokenResponse>>
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
    internal sealed class Handler : IRequestHandler<Command, Result<TokenResponse>>
    {
        private readonly ITokenService _tokenService;

        public Handler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task<Result<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return _tokenService.GetRefreshTokenAsync(request);
        }
    }
}