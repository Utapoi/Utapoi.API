using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="Lyrics" /> entity.
/// </summary>
public class LyricsEntityTypeConfiguration : IEntityTypeConfiguration<Core.Entities.Lyrics>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Core.Entities.Lyrics> builder)
    {
        builder.Property(x => x.Phrases)
            .HasConversion(
                v => string.Join("%|%", v),
                v => v.Split("%|%", StringSplitOptions.TrimEntries).ToList()
            );
    }
}