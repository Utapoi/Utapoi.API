using Karaoke.Core.Entities.Users;

namespace Karaoke.Core.Entities.Songs;

// Note(Mikyan):
// We have a name clash between the Karaoke entity and the Karaoke namespace.
// We may want to rename the entity or the project to avoid this clash.
// For now, we will use the fully qualified name of the entity.

/// <summary>
///     Represents a karaoke.
/// </summary>
public sealed class Karaoke : AuditableEntity
{
    /// <summary>
    ///     Gets or sets the path.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="User" /> who created the karaoke.
    /// </summary>
    public ICollection<User> Creators { get; } = new List<User>();
}