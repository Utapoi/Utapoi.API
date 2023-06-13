using Karaoke.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

internal sealed class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(x => x.Tokens)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);
    }
}