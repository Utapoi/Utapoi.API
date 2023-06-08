using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

public class LocalizedStringEntityTypeConfiguration : IEntityTypeConfiguration<LocalizedString>
{
    public void Configure(EntityTypeBuilder<LocalizedString> builder)
    {
    }
}