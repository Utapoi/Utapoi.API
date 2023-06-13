using System.Security.Claims;
using Karaoke.API.Requests.Auth;
using Karaoke.Application.Auth.Commands.GetRefreshToken;
using Karaoke.Application.Auth.GoogleAuth.Requests.GetAuthorizeUrl;
using Karaoke.Application.Auth.GoogleAuth.Requests.LoginRequest;
using Karaoke.Application.Auth.Responses;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Infrastructure.Options.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Karaoke.API.Controllers.Auth;

/// <summary>
///     The controller for authentication.
/// </summary>
public class AuthController : ApiControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthController" /> class.
    /// </summary>
    /// <param name="logger">
    ///     The logger.
    /// </param>
    /// <param name="environment">
    ///     The environment.
    /// </param>
    public AuthController(ILogger<AuthController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    ///     Refreshes a token.
    /// </summary>
    /// <param name="request">
    ///     The request.
    /// </param>
    /// <returns>
    ///     A <see cref="TokenResponse" /> containing the token or an error.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var result = await Mediator.Send(new GetRefreshToken.Command
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken,
                IpAddress = GetIpAddressFromRequest()
            });

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(x => x.Message));
            }

            return Ok(result.Value);
        }
        catch (ForbiddenAccessException ex)
        {
            _logger.LogError(ex, "Error refreshing token");
            return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Failed to refresh token: {Token}", request.Token);
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
    public IActionResult VerifyAsync([FromRoute] string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return BadRequest("Role cannot be null or empty.");
        }

        // Note(Mikyan): We must use the RoleManager to verify roles.
        // This will do for now.
        var roles = HttpContext.User.FindAll(ClaimTypes.Role);

        if (roles.All(r => r.Value != role))
        {
            return Unauthorized();
        }

        return Ok();
    }

    /// <summary>
    ///     Generates an authorization url for a user using Google authentication.
    /// </summary>
    /// <returns>
    ///     A <see cref="ChallengeResult" /> containing the authorization url.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("Google/Authorize")]
    [ProducesResponseType(typeof(ChallengeResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GoogleAuth()
    {
        var result = await Mediator.Send(new GetGoogleAuthorizeUrl.Request());

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return new ChallengeResult("Google", result.Value);
    }

    /// <summary>
    ///     Authorization callback for Google authentication.
    /// </summary>
    /// <param name="options">
    ///     The Google authentication options.
    /// </param>
    /// <returns>
    ///     A <see cref="RedirectResult" /> to the login success page.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("Google/AuthorizeCallback")]
    public async Task<IActionResult> GoogleAuthCallbackAsync(
        [FromServices] IOptions<GoogleAuthOptions> options
    )
    {
        var result = await Mediator.Send(new GoogleLogin.Request
        {
            IpAddress = GetIpAddressFromRequest()
        });

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        // We have to adapt the cookie options based on the environment.
        // In development, we can't send http-only cookies from the client side.
        var cookieOptions = new CookieOptions
        {
            IsEssential = true,
            HttpOnly = _environment.IsProduction(),
            SameSite = _environment.IsProduction() ? SameSiteMode.Strict : SameSiteMode.Lax,
            Secure = _environment.IsProduction()
        };

        Response.Cookies.Append(
            "AuthToken",
            JsonConvert.SerializeObject(result.Value, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                },
                Formatting = Formatting.Indented
            }), cookieOptions);

        return Redirect($"{options.Value.WebClientUrl}auth/login-result");
    }
}