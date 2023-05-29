using System.Reflection;
using Karaoke.Application.Interfaces.Persistence;
using Karaoke.Core.Entities.Songs;
using Karaoke.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Persistence;

/// <summary>
///     This is the official database used for the Karaoke application.
/// </summary>
/// <remarks>
///     We may want to consider using a separate database for community submitted songs.
///     This allow us to have a concept of "official" songs and "community" songs.
/// </remarks>
internal sealed class KaraokeDbContext : DbContext, IKaraokeDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="KaraokeDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    /// <param name="auditableEntitySaveChangesInterceptor">
    ///     The <see cref="AuditableEntitySaveChangesInterceptor" />.
    /// </param>
    public KaraokeDbContext(DbContextOptions<KaraokeDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Song" />.
    /// </summary>
    public DbSet<Song> Songs => Set<Song>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(KaraokeDbContext)) ??
                                                     Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}