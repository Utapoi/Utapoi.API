using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

public class LocalizedStringEntityTypeConfiguration : IEntityTypeConfiguration<LocalizedString>
{
    public void Configure(EntityTypeBuilder<LocalizedString> builder)
    {
    }
}