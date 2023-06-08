using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

internal sealed class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasMany(x => x.Titles);

        builder.HasMany(x => x.Singers)
            .WithMany(x => x.Albums);

        builder.HasMany(x => x.Songs)
            .WithMany(x => x.Albums);
    }
}