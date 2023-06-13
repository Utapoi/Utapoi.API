using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;
using Karaoke.Application.DTO;
using Karaoke.Application.Songs.Commands.CreateSong;
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
    private readonly ILogger<SongsController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SongsController" /> class.
    /// </summary>
    /// <param name="logger">
    ///     The logger.
    /// </param>
    public SongsController(ILogger<SongsController> logger)
    {
        _logger = logger;
    }

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
    {
        try
        {
            var result = await Mediator.Send(new GetSongs.Request(request.Skip, request.Take));

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting songs.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    ///     Creates a new song.
    /// </summary>
    /// <param name="command">
    ///     The command.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSongAsync([FromBody] CreateSong.Command command)
    {
        var result = await Mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }
}