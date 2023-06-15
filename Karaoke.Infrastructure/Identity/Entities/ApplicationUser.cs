﻿using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity.Entities;

/// <summary>
///     The application user.
/// </summary>
/// <remarks>
///     This class is used by ASP.NET Core Identity and has been extended to include a <see cref="Guid" /> as the primary
///     key.
///     All entities that need to reference the user should use <see cref="Karaoke.Core.Entities.User" /> since the
///     <see cref="ApplicationUser" /> will be only used for authentication and authorization.
/// </remarks>
public sealed class ApplicationUser : IdentityUser, IMap<ApplicationUser, User>
{
    /// <summary>
    ///     Gets or sets the profile picture.
    /// </summary>
    public string? ProfilePicture { get; set; } = string.Empty;

    public ICollection<Token> Tokens { get; set; } = new List<Token>();

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}