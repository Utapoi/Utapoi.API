using System.Reflection;
using Karaoke.Application.Persistence;
using Karaoke.Core.Entities;
using Karaoke.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Persistence.Contexts;

/// <summary>
///     This is the official database used for the Karaoke application.
/// </summary>
/// <remarks>
///     We may want to consider using a separate database for community submitted songs.
///     This allow us to have a concept of "official" songs and "community" songs.
/// </remarks>
public sealed class KaraokeDbContext : DbContext, IKaraokeDbContext
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

        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<KaraokeInfo> Karaoke => Set<KaraokeInfo>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Singer" />.
    /// </summary>
    public DbSet<Singer> Singers => Set<Singer>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Tag" />.
    /// </summary>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Album" />.
    /// </summary>
    public DbSet<Album> Albums => Set<Album>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Culture" />.
    /// </summary>
    public DbSet<Culture> Cultures => Set<Culture>();

    public DbSet<NamedFile> Files => Set<NamedFile>();

    public DbSet<KaraokeInfo> KaraokeInfos => Set<KaraokeInfo>();

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