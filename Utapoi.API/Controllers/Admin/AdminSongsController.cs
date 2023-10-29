using Microsoft.AspNetCore.Mvc;
using Utapoi.API.Extensions;
using Utapoi.Application.Common;
using Utapoi.Application.Common.Requests;
using Utapoi.Application.Songs.Requests.GetSongForEdit;
using Utapoi.Application.Songs.Requests.GetSongsForAdmin;

namespace Utapoi.API.Controllers.Admin;

/// <summary>
///  Admin songs controller.
/// </summary>
[Route("Admin/Songs")]
[Tags("Admin - Songs")]
public class AdminSongsController : ApiControllerBase
{
    /// <summary>
    ///     Gets all songs.
    /// </summary>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> of <see cref="GetSongsForAdmin.Response" />.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSongsForAdmin.Response>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSongsAsync([FromQuery] PaginatedRequest request)
        => Mediator.ProcessRequestAsync(new GetSongsForAdmin.Request(request.Skip, request.Take));

    /// <summary>
    /// Gets song for edit.
    /// </summary>
    /// <param name="id">The id of the song to edit.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="GetSongForEdit.Response"/> object.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetSongForEdit.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSongForEditAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        => Mediator.ProcessRequestAsync(new GetSongForEdit.Request(id), cancellationToken);

    /// <summary>
    ///     Updates a song.
    /// </summary>
    /// <param name="id">
    ///     The song id.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSongAsync([FromRoute] Guid id)
    {
        return Ok();
    }
}
