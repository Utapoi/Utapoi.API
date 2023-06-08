using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;
using Karaoke.Application.DTO;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Application.Singers.Requests.SearchSingers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Artists;

/// <summary>
///     The controller for singers.
/// </summary>
[Authorize(Roles = "User")]
public class SingersController : ApiControllerBase
{
    /// <summary>
    ///     Gets a list of singers.
    /// </summary>
    /// <param name="request">
    ///     The request.
    /// </param>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> containing the singers or an error.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<SingerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSingersAsync([FromQuery] PaginatedRequest request)
    {
        var m = new GetSingers.Request
        {
            Skip = request.Skip,
            Take = request.Take
        };

        var result = await Mediator.Send(m);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }

    /// <summary>
    ///     Search a singer by name.
    /// </summary>
    /// <param name="input">
    ///     The input.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPost("Search")]
    [ProducesResponseType(typeof(IEnumerable<SingerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchSingersAsync([FromQuery] string input)
    {
        var m = new SearchSingers.Request
        {
            Input = input
        };

        var result = await Mediator.Send(m);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }

    /// <summary>
    ///     Gets a singer by id.
    /// </summary>
    /// <param name="id">
    ///     The id.
    /// </param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpGet("{id:guid}")]
    public IActionResult GetSingerAsync(Guid id)
    {
        return Ok();
    }

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
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSingerAsync([FromBody] CreateSinger.Command command)
    {
        var result = await Mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest();
        }

        return CreatedAtAction("GetSinger", new { id = result.Value }, result.Value);
    }
}