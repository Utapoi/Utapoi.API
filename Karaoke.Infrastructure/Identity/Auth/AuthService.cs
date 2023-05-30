using Karaoke.Application.Auth.Commands.RegisterUser;
using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Common.Models;
using Karaoke.Application.Identity.Auth;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity.Auth;

internal sealed class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> LoginUserAsync(
        LoginUser.Request request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure("Invalid username or password");
    }

    public async Task<Result> CreateUserAsync(
        RegisterUser.Command request,
        CancellationToken cancellationToken = default
    )
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(string.Join(",", result.Errors.Select(x => x.Description)));
    }
}