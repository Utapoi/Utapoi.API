using Karaoke.API.Extensions;
using Karaoke.API.Requests.Singers;
using Karaoke.Application.DTO;
using Karaoke.Application.Singers.Requests.GetSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Application.Singers.Requests.SearchSingers;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Artists;

/// <summary>
///     Singers controller.
/// </summary>
//[Authorize(Roles = Roles.User)]
public class SingersController : ApiControllerBase
{
    private readonly ILogger<SingersController> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SingersController" /> class.
    /// </summary>
    /// <param name="logger">
    ///     The <see cref="ILogger{TCategoryName}" />.
    /// </param>
    public SingersController(ILogger<SingersController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///    Gets a list of singers.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///    A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetSingers.Response>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSingersAsync([FromQuery] GetSingersRequest request, CancellationToken cancellationToken = default)
        => Mediator.ProcessRequestAsync(new GetSingers.Request(request.Skip, request.Take));

    /// <summary>
    ///     Gets a singer by id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>
    ///     A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetSinger.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSingerAsync(Guid id)
        => Mediator.ProcessRequestAsync(new GetSinger.Request(id));

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
}