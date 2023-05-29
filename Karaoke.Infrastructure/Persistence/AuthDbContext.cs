using Duende.IdentityServer.EntityFramework.Options;
using Karaoke.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Karaoke.Infrastructure.Persistence;

/// <summary>
///     The authentication database context.
/// </summary>
/// <remarks>
///     This context is only used for authentication and authorization.
/// </remarks>
public sealed class AuthDbContext : ApiAuthorizationDbContext<ApplicationUser>
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
    public AuthDbContext(DbContextOptions<AuthDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<ApplicationUser>()
        //    .Property(x => x.Languages)
        //    .HasConversion(
        //        x => x.Select(c => c.IetfLanguageTag),
        //        x => x.Select(CultureInfo.GetCultureInfo).ToList()
        //    );

        base.OnModelCreating(builder);
    }
}