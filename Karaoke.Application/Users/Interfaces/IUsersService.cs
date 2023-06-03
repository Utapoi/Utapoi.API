using FluentResults;
using Karaoke.Application.Users.Requests.GetCurrentUser;

namespace Karaoke.Application.Users.Interfaces;

public interface IUsersService
{
    Task<Result<GetCurrentUser.Response>> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}