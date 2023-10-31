using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="Song" /> entity.
/// </summary>
public class SongEntityTypeConfiguration : IEntityTypeConfiguration<Song>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasMany(x => x.Titles)
            .WithMany();

        builder.Property(x => x.Duration)
            .HasConversion<long>();

        builder.HasOne(x => x.SongFile)
            .WithMany()
            .HasForeignKey(x => x.SongFileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Thumbnail)
            .WithMany()
            .HasForeignKey(x => x.ThumbnailId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Singers)
            .WithMany(x => x.Songs);

        builder.HasMany(x => x.Composers)
            .WithMany(x => x.Songs);

        builder.HasMany(x => x.SongWriters)
            .WithMany(x => x.Songs);

        builder.HasMany(x => x.Sources)
            .WithMany();

        builder.HasMany(x => x.Collections)
            .WithMany(x => x.Songs);

        builder.HasMany(x => x.Tags)
            .WithMany();
    }
}