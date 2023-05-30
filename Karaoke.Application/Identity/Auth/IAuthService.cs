using Karaoke.Application.Auth.Commands.RegisterUser;
using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Common.Models;

namespace Karaoke.Application.Identity.Auth;

public interface IAuthService
{
    Task<Result> CreateUserAsync(
        RegisterUser.Command request,
        CancellationToken cancellationToken = default
    );

    Task<Result> LoginUserAsync(
        LoginUser.Request request,
        CancellationToken cancellationToken = default
    );
}