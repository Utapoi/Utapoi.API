using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Auth.Requests.RegisterUser;
using Karaoke.Application.Common.Models;

namespace Karaoke.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(RegisterUser.Request request,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteUserAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<(Result Result, string UserId)> LoginUserAsync(LoginUser.Request request);
}