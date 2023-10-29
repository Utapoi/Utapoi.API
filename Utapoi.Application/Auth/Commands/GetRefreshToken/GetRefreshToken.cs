using FluentResults;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Utapoi.Application.Auth.Responses;
using Utapoi.Application.Identity.Tokens;

namespace Utapoi.Application.Auth.Commands.GetRefreshToken;

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
        public async Task<Result<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return Result.Fail("Not implemented yet.");
        }
    }
}