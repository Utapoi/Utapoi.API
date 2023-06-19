using Karaoke.Application.Auth.Commands.GetRefreshToken;
using Karaoke.Application.Auth.Commands.VerifyRole;
using Karaoke.Application.Auth.GoogleAuth.Requests.GetAuthorizeUrl;
using Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Application.Users.Interfaces;
using Karaoke.Infrastructure.Options.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Client.AspNetCore;

namespace Karaoke.API.Controllers.Auth;

/// <summary>
///     The controller for authentication.
/// </summary>
public class AuthController : ApiControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    private readonly ILogger<AuthController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUserService">The current user service.</param>
    public AuthController(
        ILogger<AuthController> logger,
        ICurrentUserService currentUserService
    )
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    /// <summary>
    ///     Refreshes a token.
    /// </summary>
    /// <returns>
    ///     An <see cref="OkResult" /> if the token was refreshed, otherwise an <see cref="BadRequestResult" />.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        if (!Request.Cookies.TryGetValue("Karaoke-Token", out var token))
        {
            return BadRequest("Token is missing.");
        }

        if (!Request.Cookies.TryGetValue("Karaoke-RefreshToken", out var refreshToken))
        {
            return BadRequest("RefreshToken is missing.");
        }

        try
        {
            var result = await Mediator.Send(new GetRefreshToken.Command
            {
                Token = token,
                RefreshToken = refreshToken,
                IpAddress = GetIpAddressFromRequest()
            });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(x => x.Message));
            }

            return Ok(result.Value);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Failed to refresh token: {Token}", token);
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing token");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    ///     Verifies that a user has a role.
    /// </summary>
    /// <param name="role">
    ///     The role to verify.
    /// </param>
    /// <returns>
    ///     An <see cref="OkResult" /> if the user has the role, otherwise an <see cref="UnauthorizedResult" />.
    /// </returns>
    [Authorize]
    [HttpPost("Verify/{role}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> VerifyAsync([FromRoute] string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            return Unauthorized();
        }

        try
        {
            var result = await Mediator.Send(new VerifyRole.Command
            {
                // The validator will throw if UserId is empty anyway.
                UserId = _currentUserService.UserId ?? string.Empty,
                Role = role
            });

            if (result.IsFailed)
            {
                return Unauthorized();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying role: {Role}", role);
            return Unauthorized();
        }
    }

    /// <summary>
    ///     Generates an authorization url for a user using Google authentication.
    /// </summary>
    /// <returns>
    ///     A <see cref="ChallengeResult" /> containing the authorization url.
    /// </returns>
    [HttpGet("Google/Authorize")]
    [ProducesResponseType(typeof(ChallengeResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GoogleAuth()
    {
        var result = await Mediator.Send(new GetGoogleAuthorizeUrl.Request());

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        var p = new AuthenticationProperties(new Dictionary<string, string?>
        {
            [OpenIddictClientAspNetCoreConstants.Properties.ProviderName] = result.Value.Provider,
        })
        {
            RedirectUri = result.Value.RedirectUrl,
            AllowRefresh = result.Value.AllowRefresh
        };

        return Challenge(p, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
    }

    /// <summary>
    ///     Authorization callback for Google authentication.
    /// </summary>
    /// <returns>
    ///     A <see cref="RedirectResult" /> to the login success page.
    /// </returns>
    [HttpGet("Google/AuthorizeCallback")]
    public async Task<IActionResult> GoogleAuthCallbackAsync()
    {
        var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        return Ok();

        // var result = await Mediator.Send(new GoogleLogin.Request
        // {
        //     IpAddress = GetIpAddressFromRequest()
        // });
        //
        // if (result.IsFailed)
        // {
        //     return BadRequest(result.Errors);
        // }
        //
        // var cookieOptions = new CookieOptions
        // {
        //     IsEssential = true,
        //     SameSite = SameSiteMode.Lax,
        //     Secure = true,
        //     HttpOnly = true
        // };
        //
        // Response.Cookies.Append("Karaoke-Token", result.Value.Token, cookieOptions);
        // Response.Cookies.Append("Karaoke-RefreshToken", result.Value.RefreshToken, cookieOptions);
        //
        // return Redirect($"{options.Value.WebClientUrl}auth/login-result");
    }
}