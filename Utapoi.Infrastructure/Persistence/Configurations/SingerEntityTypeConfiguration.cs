using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

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

        builder
            .HasMany(x => x.Descriptions)
            .WithMany();

        builder.HasMany(x => x.Activities)
            .WithMany();

        builder.HasOne(x => x.ProfilePicture)
            .WithMany()
            .HasForeignKey(x => x.ProfilePictureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cover)
            .WithMany()
            .HasForeignKey(x => x.CoverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}