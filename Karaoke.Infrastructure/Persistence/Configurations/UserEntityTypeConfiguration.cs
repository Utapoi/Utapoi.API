using System.Globalization;
using Karaoke.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karaoke.Infrastructure.Persistence.Configurations;

/// <summary>
///     Configuration for <see cref="User" /> entity.
/// </summary>
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Languages)
            .HasConversion(
                x => x.Select(c => c.IetfLanguageTag),
                x => x.Select(CultureInfo.GetCultureInfo).ToList()
            );
    }
}