using Microsoft.AspNetCore.Mvc;
using Utapoi.API.Extensions;
using Utapoi.Application.DTO;
using Utapoi.Application.Songs.Commands.CreateSong;
using Utapoi.Application.Songs.Requests.GetSong;

namespace Utapoi.API.Controllers.Songs;

/// <summary>
///     Songs controller.
/// </summary>
public sealed class SongsController : ApiControllerBase
{
    /// <summary>
    ///    Gets a song by id.
    /// </summary>
    /// <param name="id">The id of the song.</param>
    /// <returns>
    ///    A <see cref="SongDTO" /> containing the song information.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetSong.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSongAsync([FromRoute] Guid id)
        => await Mediator.ProcessRequestAsync(new GetSong.Request(id));

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
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSongAsync([FromBody] CreateSong.Command command)
        => await Mediator.ProcessRequestAsync(command);
}