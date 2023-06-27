using Karaoke.Application.Common.Requests;
using Karaoke.Application.Common;
using Karaoke.Application.DTO.Albums;
using Microsoft.AspNetCore.Mvc;
using Karaoke.Application.Albums.Requests.GetAlbumsForAdmin;
using FluentResults;
using Karaoke.API.Extensions;

namespace Karaoke.API.Controllers.Admin;

/// <summary>
///    Admin albums controller.
/// </summary>
[Route("Admin/Albums")]
[Tags("Admin - Albums")]
public sealed class AdminAlbumsController : ApiControllerBase
{
    private readonly ILogger<AdminAlbumsController> _logger;

    /// <summary>
    ///    Initializes a new instance of the <see cref="AdminAlbumsController" /> class.
    /// </summary>
    /// <param name="logger"></param>
    public AdminAlbumsController(ILogger<AdminAlbumsController> logger)
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
    [ProducesResponseType(typeof(PaginatedResponse<GetAlbumsForAdmin.Response>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetAlbumsForAdminAsync([FromQuery] PaginatedRequest request)
        => Mediator.ProcessRequestAsync(new GetAlbumsForAdmin.Request(request.Skip, request.Take));
}
