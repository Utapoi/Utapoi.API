using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;

namespace Karaoke.Application.DTO;

public class AlbumDTO : IMap<Album, AlbumDTO>
{
    public Guid Id { get; set; }

    public IEnumerable<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    public ICollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

    public ICollection<SongDTO> Songs { get; set; } = new List<SongDTO>();
}