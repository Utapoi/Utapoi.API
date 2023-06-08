using Karaoke.Core.Entities;
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
        builder.HasMany(x => x.Names);

        builder.HasMany(x => x.Nicknames);
    }
}