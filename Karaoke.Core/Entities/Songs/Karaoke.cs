using Karaoke.Core.Entities.Users;

namespace Karaoke.Core.Entities.Songs;

// Note(Mikyan):
// We have a name clash between the Karaoke entity and the Karaoke namespace.
// We may want to rename the entity or the project to avoid this clash.
// For now, we will use the fully qualified name of the entity.

public sealed class Karaoke : AuditableEntity
{
    public string Path { get; set; } = string.Empty;

    public ICollection<User> Creators { get; } = new List<User>();
}