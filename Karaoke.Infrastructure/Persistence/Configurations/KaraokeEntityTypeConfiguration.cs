using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

public class KaraokeEntityTypeConfiguration : IEntityTypeConfiguration<Core.Entities.Songs.Karaoke>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Songs.Karaoke> builder)
    {
        builder.HasMany(x => x.Creators)
            .WithMany(x => x.Karaoke);
    }
}