using FluentResults;
using Karaoke.Application.Albums.Requests.GetAlbums;
using Karaoke.Application.Albums.Requests.SearchAlbums;
using Karaoke.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Albums;

/// <summary>
///     Albums controller.
/// </summary>
public class AlbumsController : ApiControllerBase
{
    /// <summary>
    ///     Gets all albums.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="AlbumDTO" />.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlbumDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAlbumsAsync()
    {
        var result = await Mediator.Send(new GetAlbums.Request());

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
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
    public async Task<IActionResult> SearchAsync([FromQuery] string query)
    {
        var result = await Mediator.Send(new SearchAlbums.Request { Input = query });

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }
}