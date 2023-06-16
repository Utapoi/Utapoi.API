using Karaoke.API.Extensions;
using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;
using Karaoke.Application.DTO;
using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Songs.Requests.GetSong;
using Karaoke.Application.Songs.Requests.GetSongs;
using Karaoke.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Songs;

/// <summary>
///     Songs controller.
/// </summary>
[Authorize(Roles = Roles.User)]
public sealed class SongsController : ApiControllerBase
{
    /// <summary>
    ///     Gets all songs.
    /// </summary>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> of <see cref="SongDTO" />.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<SongDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSongsAsync([FromQuery] PaginatedRequest request)
        => await Mediator.ProcessAsync(new GetSongs.Request(request.Skip, request.Take));

    /// <summary>
    ///    Gets a song by id.
    /// </summary>
    /// <param name="id">The id of the song.</param>
    /// <returns>
    ///    A <see cref="SongDTO" /> containing the song information.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SongDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSongAsync([FromRoute] Guid id)
        => await Mediator.ProcessAsync(new GetSong.Request(id));

    /// <summary>
    ///     Creates a new song.
    /// </summary>
    /// <param name="command">
    ///    The <see cref="CreateSong.Command" /> containing the song information.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the id of the created song.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSongAsync([FromBody] CreateSong.Command command)
        => await Mediator.ProcessAsync(command);

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
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSongAsync([FromRoute] Guid id)
    {
        return Ok();
    }
}