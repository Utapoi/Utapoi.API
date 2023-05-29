using Karaoke.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="Work" /> entity.
/// </summary>
public class WorkEntityTypeConfiguration : IEntityTypeConfiguration<Work>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.OwnsMany(x => x.Names);

        builder.OwnsMany(x => x.Descriptions);
    }
}