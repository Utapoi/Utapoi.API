using System.Security.Claims;
using FluentResults;
using Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Identity.GoogleAuth;
using Karaoke.Application.Identity.Tokens;
using Karaoke.Core.Common;
using Karaoke.Infrastructure.Identity.Entities;
using Karaoke.Infrastructure.Identity.Extensions;
using Karaoke.Infrastructure.Options.Admin;
using Karaoke.Infrastructure.Options.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OpenIddict.Client.WebIntegration;

namespace Karaoke.Infrastructure.Identity.Auth;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly AdminOptions _adminOptions;

    private readonly GoogleAuthOptions _googleAuthOptions;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly ITokenService _tokenService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GoogleAuthService" /> class.
    /// </summary>
    /// <param name="signInManager">
    ///     The sign in manager.
    /// </param>
    /// <param name="adminOptions">
    ///     The admin options.
    /// </param>
    /// <param name="googleAuthOptions">
    ///     The google auth options.
    /// </param>
    /// <param name="tokenService">
    ///     The token service.
    /// </param>
    public GoogleAuthService(
        IOptions<AdminOptions> adminOptions,
        IOptions<GoogleAuthOptions> googleAuthOptions,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService
    )
    {
        _adminOptions = adminOptions.Value;
        _googleAuthOptions = googleAuthOptions.Value;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public GoogleAuthProperties GetAuthorizeUrl()
    {
        return new GoogleAuthProperties
        {
            Provider = OpenIddictClientWebIntegrationConstants.Providers.Google,
            RedirectUrl = _googleAuthOptions.RedirectUrl,
            AllowRefresh = true
        };
    }

    public async Task<Result<TokenResponse>> LoginAsync(
        GoogleLogin.Request request,
        CancellationToken cancellationToken = default
    )
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info == null)
        {
            return Result.Fail("Failed to get external login info.");
        }

        var result = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider,
            info.ProviderKey,
            false
        );

        if (!result.Succeeded)
        {
            // Try to register the user.
            return await RegisterAsync(request, cancellationToken);
        }

        var user = await _signInManager.UserManager.FindByLoginAsync(
            info.LoginProvider,
            info.ProviderKey
        );

        if (user == null)
        {
            return Result.Fail("Failed to find user.");
        }

        var token = await _tokenService.GetTokenAsync(
            info.LoginProvider,
            info.ProviderKey,
            request.IpAddress,
            cancellationToken
        );

        await _signInManager.UserManager.UpdateAsync(user);

        return token;
    }

    public async Task<Result<TokenResponse>> RegisterAsync(GoogleLogin.Request request,
        CancellationToken cancellationToken = default)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info == null)
        {
            return Result.Fail("Failed to get external login info.");
        }

        var user = new ApplicationUser
        {
            UserName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
            ProfilePicture = info.Principal.FindFirstValue(GoogleClaimTypes.ProfilePicture),
            EmailConfirmed = info.Principal.FindFirstValue<bool>(GoogleClaimTypes.EmailVerified)
        };


        var result = await _signInManager.UserManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return Result.Fail("Failed to create user.");
        }


        result = await _signInManager.UserManager.AddLoginAsync(user, info);

        if (!result.Succeeded)
        {
            return Result.Fail("Failed to add login.");
        }

        await _signInManager.UserManager.AddToRoleAsync(user, Roles.User);
        await _signInManager.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, Roles.User));

        if (_adminOptions.AllowedEmails.Contains(user.Email))
        {
            await _signInManager.UserManager.AddToRoleAsync(user, Roles.Admin);
            await _signInManager.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, Roles.Admin));
        }

        if (!result.Succeeded)
        {
            return Result.Fail("Failed to add user to role.");
        }

        return await _tokenService.GetTokenAsync(
            info.LoginProvider,
            info.ProviderKey,
            request.IpAddress,
            cancellationToken
        );
    }
}