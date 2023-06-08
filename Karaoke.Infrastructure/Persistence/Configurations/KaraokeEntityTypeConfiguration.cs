using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

public class KaraokeEntityTypeConfiguration : IEntityTypeConfiguration<KaraokeInfo>
{
    public void Configure(EntityTypeBuilder<KaraokeInfo> builder)
    {
        builder.HasMany(x => x.Creators)
            .WithMany(x => x.Karaoke);
    }
}