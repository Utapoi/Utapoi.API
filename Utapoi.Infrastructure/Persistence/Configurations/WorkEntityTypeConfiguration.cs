using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="Work" /> entity.
/// </summary>
public class WorkEntityTypeConfiguration : IEntityTypeConfiguration<Work>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.HasMany(x => x.Names)
            .WithMany();

        builder.HasMany(x => x.Descriptions)
            .WithMany();
    }
}