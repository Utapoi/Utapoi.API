using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Auth;

/// <summary>
///     The controller for admin actions.
/// </summary>
[Authorize(Roles = "Admin")]
public class AdminController : ApiControllerBase
{
    private readonly ILogger<AdminController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AdminController" /> class.
    /// </summary>
    /// <param name="logger"></param>
    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Verifies that the user is an admin.
    /// </summary>
    /// <returns>
    ///     A 200 OK response in case of success.
    /// </returns>
    [HttpPost("Verify")]
    public IActionResult Verify()
    {
        return Ok();
    }
}