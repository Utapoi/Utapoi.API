using Karaoke.Application.Common.Requests;
using Karaoke.Application.Common;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Microsoft.AspNetCore.Mvc;
using Karaoke.Application.DTO;

namespace Karaoke.API.Controllers.Admin;

/// <summary>
///   Admin singers controller.
/// </summary>
[Route("Admin/Singers")]
[Tags("Admin - Singers")]
public sealed class AdminSingersController : ApiControllerBase
{
    private readonly ILogger<AdminSingersController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AdminSingersController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public AdminSingersController(ILogger<AdminSingersController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Gets a list of singers.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> containing the singers.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<SingerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSingersAsync([FromQuery] PaginatedRequest request)
    {
        try
        {
            var result = await Mediator.Send(new GetSingersForAdmin.Request(request.Skip, request.Take));

            if (result.IsFailed)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting singers.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
