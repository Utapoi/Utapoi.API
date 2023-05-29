using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Identity;

/// <summary>
///     The application user.
/// </summary>
/// <remarks>
///     This class is used by ASP.NET Core Identity and has been extended to include a <see cref="Guid" /> as the primary
///     key.
///     All entities that need to reference the user should use <see cref="Karaoke.Core.Entities.Users.User" /> since the
///     <see cref="ApplicationUser" />
///     will be only used for authentication and authorization.
/// </remarks>
public class ApplicationUser : IdentityUser<Guid>
{
}