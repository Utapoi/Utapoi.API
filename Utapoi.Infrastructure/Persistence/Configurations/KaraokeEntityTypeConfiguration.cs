using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

public class KaraokeEntityTypeConfiguration : IEntityTypeConfiguration<KaraokeInfo>
{
    public void Configure(EntityTypeBuilder<KaraokeInfo> builder)
    {
        builder.HasMany(x => x.Creators)
            .WithMany(x => x.Karaoke);
    }
}