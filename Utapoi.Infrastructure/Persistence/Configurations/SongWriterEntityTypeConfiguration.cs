using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configures the <see cref="SongWriter" /> entity.
/// </summary>
public class SongWriterEntityTypeConfiguration : IEntityTypeConfiguration<SongWriter>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SongWriter> builder)
    {
        builder.HasMany(x => x.Names)
            .WithMany();

        builder.HasMany(x => x.Nicknames)
            .WithMany();
    }
}