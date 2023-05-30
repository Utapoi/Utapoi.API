using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Auth.Requests.RegisterUser;
using Karaoke.Application.Identity.Tokens;

namespace Karaoke.Application.Identity.Auth;

public interface IAuthService
{
    Task<TokenResponse?> CreateUserAsync(
        RegisterUser.Request request,
        CancellationToken cancellationToken = default
    );

    Task<TokenResponse?> LoginUserAsync(
        LoginUser.Request request,
        CancellationToken cancellationToken = default
    );
}