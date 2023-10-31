using Microsoft.AspNetCore.Mvc;
using Utapoi.API.Extensions;
using Utapoi.Application.Common;
using Utapoi.Application.Common.Requests;
using Utapoi.Application.Singers.Commands.CreateSinger;
using Utapoi.Application.Singers.Commands.DeleteSinger;
using Utapoi.Application.Singers.Commands.EditSinger;
using Utapoi.Application.Singers.Requests.GetSingersForAdmin;

namespace Utapoi.API.Controllers.Admin;

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
    [ProducesResponseType(typeof(CreateSinger.Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSingerAsync([FromBody] CreateSinger.Command command)
    {
        var result = await Mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return StatusCode(StatusCodes.Status201Created, new { result.Value.Id });
    }

    /// <summary>
    ///    Deletes a singer.
    /// </summary>
    /// <param name="id">The id of the singer to delete.</param>
    /// <returns>
    ///    A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSingerAsync([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteSinger.Command
        {
            Id = id
        });

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok();
    }

    /// <summary>
    ///    Edits a singer.
    /// </summary>
    /// <param name="id">The singer id.</param>
    /// <param name="command">The request.</param>
    /// <returns>
    ///    A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(EditSinger.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> EditSingerAsync([FromRoute] Guid id, [FromBody] EditSinger.Command command)
    {
        command.SingerId = id;

        return Mediator.ProcessCommandAsync(command);
    }

    /// <summary>
    ///     Gets a list of singers.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> containing the singers.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSingersForAdmin.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSingersAsync([FromQuery] PaginatedRequest request)
        => Mediator.ProcessRequestAsync(new GetSingersForAdmin.Request(request.Skip, request.Take));
}
