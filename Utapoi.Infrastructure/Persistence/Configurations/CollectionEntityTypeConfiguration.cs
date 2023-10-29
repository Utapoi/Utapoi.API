using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

public class CollectionEntityTypeConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.HasMany(x => x.Names)
            .WithMany()
            .UsingEntity("CollectionNamesLocalizedString");

        builder.HasMany(x => x.Tags)
            .WithMany();
    }
}