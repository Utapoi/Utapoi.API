﻿using System.Security.Claims;
using Utapoi.Application.Users.Interfaces;

namespace Utapoi.API.Services;

/// <summary>
///     A service to get the current user.
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CurrentUserService" /> class.
    /// </summary>
    /// <param name="httpContextAccessor">
    ///     The HTTP context accessor.
    /// </param>
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Gets the user identifier.
    /// </summary>
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}