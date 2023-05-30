using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Auth.Requests.RegisterUser;
using Karaoke.Application.Identity.Auth;
using Karaoke.Application.Identity.Tokens;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity.Auth;

internal sealed class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        SignInManager<ApplicationUser> signInManager
    )
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<TokenResponse?> CreateUserAsync(
        RegisterUser.Request request,
        CancellationToken cancellationToken = default
    )
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return await _tokenService.GetTokenAsync(
                new TokenRequest(request.Username, request.Password),
                request.IpAddress,
                cancellationToken
            );
        }

        return null;
    }

    public async Task<TokenResponse?> LoginUserAsync(
        LoginUser.Request request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        if (result.Succeeded)
        {
            return await _tokenService.GetTokenAsync(
                new TokenRequest(request.Username, request.Password),
                request.IpAddress,
                cancellationToken
            );
        }

        return null;
    }
}