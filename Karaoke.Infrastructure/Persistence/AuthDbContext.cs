using Karaoke.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Karaoke.Infrastructure.Persistence;

/// <summary>
///     The authentication database context.
/// </summary>
/// <remarks>
///     This context is only used for authentication and authorization.
/// </remarks>
public sealed class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    /// <param name="operationalStoreOptions">
    ///     The <see cref="IOptions{OperationalStoreOptions}" />.
    /// </param>
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
}