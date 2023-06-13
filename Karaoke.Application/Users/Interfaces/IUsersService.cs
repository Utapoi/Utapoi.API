using FluentResults;
using Karaoke.Application.Users.Requests.GetCurrentUser;

namespace Karaoke.Application.Users.Interfaces;

public interface IUsersService
{
    Task<Result<GetCurrentUser.Response>> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Check if user is in a specific role.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="role">The role name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result" /> indicating whether the user is in the specified role.
    /// </returns>
    Task<Result> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken = default);
}