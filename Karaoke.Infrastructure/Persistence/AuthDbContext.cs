using System.Globalization;
using Karaoke.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Persistence;

/// <summary>
///     The authentication database context.
/// </summary>
/// <remarks>
///     This context is only used for authentication and authorization.
/// </remarks>
internal sealed class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
            .Property(x => x.Languages)
            .HasConversion(
                x => x.Select(c => c.IetfLanguageTag),
                x => x.Select(CultureInfo.GetCultureInfo).ToList()
            );

        base.OnModelCreating(builder);
    }
}