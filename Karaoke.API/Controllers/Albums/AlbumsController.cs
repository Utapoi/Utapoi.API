using FluentResults;
using Karaoke.Application.Albums.Commands.CreateAlbum;
using Karaoke.Application.Albums.Requests.SearchAlbums;
using Karaoke.Application.DTO.Albums;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Albums;

/// <summary>
///     Albums controller.
/// </summary>
//[Authorize(Roles = Roles.User)]
public class AlbumsController : ApiControllerBase
{
    private readonly ILogger<AlbumsController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AlbumsController" /> class.
    /// </summary>
    public AlbumsController(ILogger<AlbumsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Searches albums.
    /// </summary>
    /// <param name="query">
    ///     The query.
    /// </param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="AlbumDTO" />.
    /// </returns>
    [HttpPost("Search")]
    [ProducesResponseType(typeof(IEnumerable<AlbumDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAlbumsAsync([FromQuery] string query)
    {
        var result = await Mediator.Send(new SearchAlbums.Request { Input = query });

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    /// <summary>
    ///     Creates an album.
    /// </summary>
    /// <param name="command">
    ///     The command.
    /// </param>
    /// <returns>
    ///     A <see cref="string" /> of the album id.
    /// </returns>
    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAlbumAsync([FromBody] CreateAlbum.Command command)
    {
        try
        {
            var result = await Mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating album");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}