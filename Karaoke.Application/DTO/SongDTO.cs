using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities.Songs;

namespace Karaoke.Application.DTO;

/// <summary>
///     Represents a song.
/// </summary>
public sealed class SongDTO : IMap<Song, SongDTO>
{
    public Guid Id { get; set; }
}