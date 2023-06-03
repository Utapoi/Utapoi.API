using Karaoke.API.Requests.Auth;
using Karaoke.Application.Auth.Commands.RefreshToken;
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
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthController" /> class.
    /// </summary>
    /// <param name="logger">
    ///     The logger.
    /// </param>
    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
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
    [Authorize]
    [HttpPost("RefreshToken")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var m = new RefreshToken.Command
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken,
                IpAddress = GetOriginFromRequest()
            };

            var response = await Mediator.Send(m);

            return Ok(response);
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
        var result = await Mediator.Send(new GoogleLogin.Request());

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddMinutes(2),
            IsEssential = true
        };

        Response.Cookies.Append(
            "AuthToken",
            JsonConvert.SerializeObject(result.Value, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            }), cookieOptions);

        return Redirect($"{options.Value.WebClientUrl}auth/login-result");
    }
}