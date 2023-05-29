using Karaoke.Core.Entities.Artists;
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
        builder.OwnsMany(x => x.Names);

        builder.OwnsMany(x => x.Nicknames);
    }
}