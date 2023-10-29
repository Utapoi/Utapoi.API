﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utapoi.Application.Users.Requests.GetCurrentUser;

namespace Utapoi.API.Controllers.Users;

/// <summary>
///     Users controller
/// </summary>
[Authorize]
public class UsersController : ApiControllerBase
{
    /// <summary>
    ///     Get current user
    /// </summary>
    /// <returns>
    ///     Returns current user
    /// </returns>
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var result = await Mediator.Send(new GetCurrentUser.Request());

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value.User);
    }
}