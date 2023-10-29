using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Infrastructure.Identity.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

internal sealed class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.AccessToken)
            .WithOne()
            .HasForeignKey<RefreshToken>(x => x.TokenId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}