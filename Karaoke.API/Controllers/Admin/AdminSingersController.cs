using Karaoke.Application.Common.Requests;
using Karaoke.Application.Common;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Microsoft.AspNetCore.Mvc;
using Karaoke.Application.DTO;
using Karaoke.API.Extensions;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.API.Controllers.Artists;

namespace Karaoke.API.Controllers.Admin;

/// <summary>
///   Admin singers controller.
/// </summary>
[Route("Admin/Singers")]
[Tags("Admin - Singers")]
public sealed class AdminSingersController : ApiControllerBase
{
    /// <summary>
    ///     Creates a new singer.
    /// </summary>
    /// <param name="command">
    ///     The request.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateSingerAsync([FromBody] CreateSinger.Command command)
        => Mediator.ProcessCreateCommandAsync(
            command,
            nameof(SingersController),
            nameof(SingersController.GetSingerAsync)
        );

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
    public Task<IActionResult> GetSingersAsync([FromQuery] PaginatedRequest request)
        => Mediator.ProcessRequestAsync(new GetSingersForAdmin.Request(request.Skip, request.Take));
}
