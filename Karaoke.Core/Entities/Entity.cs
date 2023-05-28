using System.ComponentModel.DataAnnotations;

namespace Karaoke.Core.Entities;

/// <summary>
///     Represents an entity.
/// </summary>
public abstract class Entity
{
    /// <summary>
    ///     Gets or sets the identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}