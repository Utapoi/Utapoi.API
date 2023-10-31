﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configures the <see cref="Composer" /> entity.
/// </summary>
public class ComposerEntityTypeConfiguration : IEntityTypeConfiguration<Composer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Composer> builder)
    {
        builder.HasMany(x => x.Names)
            .WithMany();

        builder.HasMany(x => x.Nicknames)
            .WithMany();
    }
}