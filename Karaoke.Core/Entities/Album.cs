using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

public class Album : AuditableEntity
{
    public ICollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    public Guid CoverId {get;set;}

    public NamedFile? Cover {get; set;}

    public ICollection<Singer> Singers { get; set; } = new List<Singer>();

    public ICollection<Song> Songs { get; set; } = new List<Song>();
}