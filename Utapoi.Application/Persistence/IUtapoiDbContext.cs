using Microsoft.EntityFrameworkCore;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Persistence;

public interface IUtapoiDbContext
{
    DbSet<Album> Albums { get; }

    DbSet<NamedFile> Files { get; }

    DbSet<LocalizedString> LocalizedStrings { get; }

    DbSet<Singer> Singers { get; }

    DbSet<Song> Songs { get; }

    DbSet<Tag> Tags { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}