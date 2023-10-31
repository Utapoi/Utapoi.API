using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Persistence.Configurations;

/// <summary>
///     ///     Configuration for <see cref="User" /> entity.
/// </summary>
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // No configuration for now.
    }
}