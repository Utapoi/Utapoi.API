using FluentResults;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Utapoi.Application.Users.Interfaces;

namespace Utapoi.Application.Auth.Commands.VerifyRole;

/// <summary>
///     Represents a command for verifying a user's role.
/// </summary>
public static class VerifyRole
{
    /// <summary>
    ///     The command for verifying a user's role.
    /// </summary>
    public sealed class Command : IRequest<Result>
    {
        public Command()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="userId">The id of the user to verify</param>
        /// <param name="role">The role to verify.</param>
        public Command(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        /// <summary>
        ///     The id of the user to verify.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        ///     The role to verify.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }

    /// <summary>
    ///     Validates the inputs passed to the <see cref="Command" />
    /// </summary>
    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Validator" /> class.
        /// </summary>
        public Validator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Role)
                .NotEmpty()
                .NotNull();
        }
    }

    /// <summary>
    ///     The handler for the <see cref="Command" />.
    /// </summary>
    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUsersService _usersService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="usersService">The <see cref="IUsersService" />.</param>
        public Handler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        ///     Handles the <see cref="Command" />.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> to execute.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> indicating whether the user is in the specified role.
        /// </returns>
        public Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            return _usersService.IsInRoleAsync(command.UserId, command.Role, cancellationToken);
        }
    }
}