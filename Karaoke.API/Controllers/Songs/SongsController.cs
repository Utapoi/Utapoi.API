using Karaoke.Application.DTO;
using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Songs.Requests.GetSongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Songs;

/// <summary>
///     Songs controller.
/// </summary>
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
    ///     An <see cref="IEnumerable{T}" /> of <see cref="SongDTO" />.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSongs.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var m = new GetSongs.Request();
            var response = await Mediator.Send(m);

            return Ok(response);
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSong.Command command)
    {
        var result = await Mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }
}