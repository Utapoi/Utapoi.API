using Karaoke.Core.Entities;
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
            .WithMany()
            .UsingEntity("SingerNamesLocalizedString");
            //.UsingEntity(
            //    "SingerNamesLocalizedString",
            //    l => l.HasOne(typeof(LocalizedString))
            //        .WithMany()
            //        .HasForeignKey("NamesId")
            //        .HasPrincipalKey(nameof(LocalizedString.Id)),
            //    r => r.HasOne(typeof(Singer))
            //        .WithMany()
            //        .HasForeignKey("SingerNamesId")
            //        .HasPrincipalKey(nameof(Singer.Id))
            //);

        builder.HasMany(x => x.Nicknames)
            .WithMany()
            .UsingEntity("SingerNicknamesLocalizedString");

        builder
            .HasMany(x => x.Descriptions)
            .WithMany()
            .UsingEntity("SingerDescriptionsLocalizedString");

        builder.HasMany(x => x.Activities)
            .WithMany()
            .UsingEntity("SingerActivitiesLocalizedString");

        builder.HasOne(x => x.ProfilePicture)
            .WithMany()
            .HasForeignKey(x => x.ProfilePictureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}