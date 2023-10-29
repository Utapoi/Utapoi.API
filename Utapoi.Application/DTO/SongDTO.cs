using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;

namespace Utapoi.Application.DTO;

/// <summary>
///     Represents a song.
/// </summary>
public sealed class SongDTO : IMap<Song, SongDTO>
{
    public Guid Id { get; set; }
}