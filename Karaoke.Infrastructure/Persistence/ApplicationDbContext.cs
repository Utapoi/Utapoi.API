using System.Reflection;
using Karaoke.Application.Interfaces.Persistence;
using Karaoke.Core.Entities.Songs;
using Karaoke.Infrastructure.Identity;
using Karaoke.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Persistence;

/// <summary>
///     The application database context.
/// </summary>
internal sealed class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    /// <param name="auditableEntitySaveChangesInterceptor">
    ///     The <see cref="AuditableEntitySaveChangesInterceptor" />.
    /// </param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Song" />.
    /// </summary>
    public DbSet<Song> Songs => Set<Song>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)) ??
                                                Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}