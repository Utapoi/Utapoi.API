﻿using Karaoke.Application.Common.Requests;
using Karaoke.Application.Common;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Microsoft.AspNetCore.Mvc;
using Karaoke.API.Extensions;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.API.Controllers.Artists;
using Karaoke.Application.Singers.Commands.EditSinger;

namespace Karaoke.API.Controllers.Admin;

/// <summary>
///   Admin singers controller.
/// </summary>
[Route("Admin/Singers")]
[Tags("Admin - Singers")]
public sealed class AdminSingersController : ApiControllerBase
{
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
    [ProducesResponseType(typeof(CreateSinger.Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateSingerAsync([FromBody] CreateSinger.Command command)
        => Mediator.ProcessCreateCommandAsync(
            command,
            nameof(SingersController),
            nameof(SingersController.GetSingerAsync)
        );

    /// <summary>
    ///    Edits a singer.
    /// </summary>
    /// <param name="id">The singer id.</param>
    /// <param name="command">The request.</param>
    /// <returns>
    ///    A <see cref="IActionResult" /> containing the result of the operation.
    /// </returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(EditSinger.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> EditSingerAsync([FromRoute] Guid id, [FromBody] EditSinger.Command command)
    {
        command.SingerId = id;

        return Mediator.ProcessCommandAsync(command);
    }

    /// <summary>
    ///     Gets a list of singers.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A <see cref="PaginatedResponse{T}" /> containing the singers.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSingersForAdmin.Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetSingersAsync([FromQuery] PaginatedRequest request)
        => Mediator.ProcessRequestAsync(new GetSingersForAdmin.Request(request.Skip, request.Take));
}