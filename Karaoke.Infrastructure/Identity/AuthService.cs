using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Auth.Requests.RegisterUser;
using Karaoke.Application.Common.Models;
using Karaoke.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly IAuthorizationService _authorizationService;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager
    )
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(RegisterUser.Request request,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string UserId)> LoginUserAsync(LoginUser.Request request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        return result.Succeeded
            ? (Result.Success(), _userManager.Users.Single(u => u.UserName == request.Username).Id)
            : (Result.Failure(new[]
            {
                "Invalid username of password"
            }), string.Empty);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}