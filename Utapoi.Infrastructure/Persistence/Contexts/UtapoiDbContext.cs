﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Utapoi.Application.Persistence;
using Utapoi.Core.Entities;
using Utapoi.Infrastructure.Persistence.Interceptors;
using Tag = Utapoi.Core.Entities.Tag;

namespace Utapoi.Infrastructure.Persistence.Contexts;

/// <summary>
///     This is the official database used for the Karaoke application.
/// </summary>
/// <remarks>
///     We may want to consider using a separate database for community submitted songs.
///     This allow us to have a concept of "official" songs and "community" songs.
/// </remarks>
public sealed class UtapoiDbContext : DbContext, IUtapoiDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UtapoiDbContext" /> class.
    /// </summary>
    /// <param name="options">
    ///     The <see cref="DbContextOptions{TContext}" />.
    /// </param>
    /// <param name="auditableEntitySaveChangesInterceptor">
    ///     The <see cref="AuditableEntitySaveChangesInterceptor" />.
    /// </param>
    public UtapoiDbContext(DbContextOptions<UtapoiDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        ChangeTracker.LazyLoadingEnabled = false;
    }

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Singer" />.
    /// </summary>
    public DbSet<Singer> Singers => Set<Singer>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Core.Entities.Tag" />.
    /// </summary>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Album" />.
    /// </summary>
    public DbSet<Album> Albums => Set<Album>();

    public DbSet<NamedFile> Files => Set<NamedFile>();

    public DbSet<LocalizedString> LocalizedStrings => Set<LocalizedString>();

    /// <summary>
    ///     Gets a <see cref="DbSet{TEntity}" /> of <see cref="Song" />.
    /// </summary>
    public DbSet<Song> Songs => Set<Song>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UtapoiDbContext)) ??
                                                     Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}