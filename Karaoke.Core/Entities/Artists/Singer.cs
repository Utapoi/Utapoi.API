using Karaoke.Core.Entities.Common;
using Karaoke.Core.Entities.Songs;

namespace Karaoke.Core.Entities.Artists;

public sealed class Singer : AuditableEntity
{
    public ICollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

    public ICollection<LocalizedString> Nicknames { get; set; } = new List<LocalizedString>();

    public ICollection<Song> Songs { get; set; } = new List<Song>();
}