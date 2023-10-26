using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

internal sealed class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasMany(x => x.Titles)
            .WithMany();

        builder.HasMany(x => x.Singers)
            .WithMany(x => x.Albums);

        builder.HasOne(x => x.Cover)
            .WithMany()
            .HasForeignKey(x => x.CoverId);

        builder.HasMany(x => x.Songs)
            .WithMany(x => x.Albums);
    }
}