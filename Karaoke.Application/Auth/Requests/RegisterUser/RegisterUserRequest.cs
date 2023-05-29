using FluentValidation;
using JetBrains.Annotations;
using Karaoke.Application.Interfaces.Auth;
using MediatR;

namespace Karaoke.Application.Auth.Requests.RegisterUser;

public static class RegisterUser
{
    public sealed class Request : IRequest<RegisterUserResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }

    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Request, RegisterUserResponse>
    {
        private readonly IAuthService _authService;

        public Handler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegisterUserResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var (result, userId) = await _authService.CreateUserAsync(request, cancellationToken);

            return new RegisterUserResponse
            {
                Result = result,
                UserId = userId
            };
        }
    }
}