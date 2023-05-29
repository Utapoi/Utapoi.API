using Karaoke.Core.Entities.Artists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configures the <see cref="SongWriter" /> entity.
/// </summary>
public class SongWriterEntityTypeConfiguration : IEntityTypeConfiguration<SongWriter>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SongWriter> builder)
    {
        builder.OwnsMany(x => x.Names);

        builder.OwnsMany(x => x.Nicknames);
    }
}