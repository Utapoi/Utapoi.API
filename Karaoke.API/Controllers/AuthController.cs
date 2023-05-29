using Karaoke.Application.Auth.Requests.LoginUser;
using Karaoke.Application.Auth.Requests.RegisterUser;
using Karaoke.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers;

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
    ///     Registers a new user.
    /// </summary>
    /// <param name="request">
    ///     The request.
    /// </param>
    /// <returns>
    ///     A <see cref="string" /> containing the UserId or a <see cref="IEnumerable{T}" /> of <see cref="string" />
    ///     containing the errors.
    /// </returns>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUser.Request request)
    {
        try
        {
            var response = await Mediator.Send(request);

            if (!response.Result.Succeeded)
            {
                return BadRequest(response.Result.Errors);
            }

            return Created(string.Empty, response.UserId);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Error registering in user: {Username}", request.Username);
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user: {Username} / {Email}", request.Username, request.Email);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    ///     Logs in a user.
    /// </summary>
    /// <param name="request">
    ///     The request.
    /// </param>
    /// <returns>
    ///     A <see cref="string" /> containing the UserId or a <see cref="IEnumerable{T}" /> of <see cref="string" />
    ///     containing the errors.
    /// </returns>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUser.Request request)
    {
        try
        {
            var response = await Mediator.Send(request);

            if (!response.Result.Succeeded)
            {
                return BadRequest(response.Result.Errors);
            }

            return Ok(response.UserId);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Error logging in user: {Username}", request.Username);
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error logging in user: {Username}", request.Username);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}