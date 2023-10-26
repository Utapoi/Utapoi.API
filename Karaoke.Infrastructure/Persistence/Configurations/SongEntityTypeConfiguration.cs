using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

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

        builder.HasOne(x => x.OriginalFile)
            .WithMany()
            .HasForeignKey(x => x.OriginalFileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Vocal)
            .WithMany()
            .HasForeignKey(x => x.VocalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Instrumental)
            .WithMany()
            .HasForeignKey(x => x.InstrumentalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Thumbnail)
            .WithMany()
            .HasForeignKey(x => x.ThumbnailId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Preview)
            .WithMany()
            .HasForeignKey(x => x.PreviewId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Lyrics)
            .WithOne(x => x.Song)
            .HasForeignKey(x => x.SongId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Karaoke)
            .WithOne(x => x.Song)
            .HasForeignKey(x => x.SongId)
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