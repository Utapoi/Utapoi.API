﻿using Karaoke.API.Extensions;
using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;
using Karaoke.Application.DTO;
using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Songs.Requests.GetSong;
using Karaoke.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers.Songs;

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
    [ProducesResponseType(typeof(SongDTO), StatusCodes.Status200OK)]
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