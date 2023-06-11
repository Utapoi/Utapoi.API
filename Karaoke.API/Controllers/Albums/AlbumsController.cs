using FluentResults;
using Karaoke.Application.Albums.Requests.GetAlbums;
using Karaoke.Application.Albums.Requests.SearchAlbums;
using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;
using Karaoke.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Albums;

/// <summary>
///     Albums controller.
/// </summary>
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
    ///     Gets all albums.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="AlbumDTO" />.
    /// </returns>
    [HttpGet]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(PaginatedResponse<AlbumDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAlbumsAsync([FromQuery] PaginatedRequest request)
    {
        try 
        {
            var result = await Mediator.Send(new GetAlbums.Request(request.Skip, request.Take));

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting albums");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
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
    [Authorize(Roles = "User")]
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