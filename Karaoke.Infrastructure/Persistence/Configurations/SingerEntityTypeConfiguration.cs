﻿using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configures the <see cref="Singer" /> entity.
/// </summary>
public class SingerEntityTypeConfiguration : IEntityTypeConfiguration<Singer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Singer> builder)
    {
        builder.HasMany(x => x.Names)
            .WithMany();

        builder.HasMany(x => x.Nicknames)
            .WithMany();

        builder.HasOne(x => x.ProfilePicture)
            .WithMany()
            .HasForeignKey(x => x.ProfilePictureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}