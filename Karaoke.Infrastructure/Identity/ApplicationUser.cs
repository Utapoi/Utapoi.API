using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity;

/// <summary>
///     The application user.
/// </summary>
/// <remarks>
///     This class is used by ASP.NET Core Identity and has been extended to include a <see cref="Guid" /> as the primary
///     key.
///     All entities that need to reference the user should use <see cref="Core.Entities.Users.User" /> since the
///     <see cref="ApplicationUser" /> will be only used for authentication and authorization.
/// </remarks>
public sealed class ApplicationUser : IdentityUser
{
    /// <summary>
    ///     Gets or sets the profile picture.
    /// </summary>
    public string ProfilePicture { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.Now;
}