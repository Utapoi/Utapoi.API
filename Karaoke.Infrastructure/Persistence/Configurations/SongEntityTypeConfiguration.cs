using System.Globalization;
using Karaoke.Core.Entities.Songs;
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
        builder.OwnsMany(x => x.Titles);

        builder.Property(x => x.Duration)
            .HasConversion<long>();

        builder.Property(x => x.OriginalLanguage)
            .HasConversion(
                c => c.IetfLanguageTag,
                c => CultureInfo.GetCultureInfo(c)
            );

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

        builder.OwnsMany(x => x.Sources);

        builder.HasMany(x => x.Collections)
            .WithMany(x => x.Songs);

        builder.OwnsMany(x => x.Tags);
    }
}