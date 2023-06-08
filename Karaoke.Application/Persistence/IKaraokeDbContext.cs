using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Persistence;

public interface IKaraokeDbContext
{
    DbSet<Album> Albums { get; }

    DbSet<Culture> Cultures { get; }

    DbSet<NamedFile> Files { get; }

    DbSet<KaraokeInfo> KaraokeInfos { get; }

    DbSet<Singer> Singers { get; }

    DbSet<Song> Songs { get; }

    DbSet<Tag> Tags { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}