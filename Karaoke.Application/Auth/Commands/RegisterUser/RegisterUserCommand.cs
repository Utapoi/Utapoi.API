using FluentValidation;
using JetBrains.Annotations;
using Karaoke.Application.Common.Models;
using Karaoke.Application.Identity.Auth;
using MediatR;

namespace Karaoke.Application.Auth.Commands.RegisterUser;

public static class RegisterUser
{
    public sealed class Command : IRequest<Result>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;
    }

    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.IpAddress).NotEmpty();
        }
    }

    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly IAuthService _authService;

        public Handler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            return _authService.CreateUserAsync(request, cancellationToken);
        }
    }
}