using Karaoke.Core.Entities.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="Lyrics" /> entity.
/// </summary>
public class LyricsEntityTypeConfiguration : IEntityTypeConfiguration<Lyrics>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Lyrics> builder)
    {
    }
}