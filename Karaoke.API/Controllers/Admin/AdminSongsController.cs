using Karaoke.Application.Common.Requests;
using Karaoke.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Karaoke.API.Extensions;
using Karaoke.Application.Songs.Requests.GetSongsForAdmin;
using Karaoke.Core.Common;
using Microsoft.AspNetCore.Authorization;

namespace Karaoke.API.Controllers.Admin;

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
