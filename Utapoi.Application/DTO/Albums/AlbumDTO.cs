using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;

namespace Utapoi.Application.DTO.Albums;

/// <summary>
///     Represents the data transfer object for <see cref="Album" />.
/// </summary>
public sealed class AlbumDTO : IMap<Album, AlbumDTO>
{
    public string Id { get; set; } = string.Empty;

    public IEnumerable<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    public ICollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

    public ICollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

    /// <inheritdoc />
    public void ConfigureMapping(IMappingExpression<Album, AlbumDTO> map)
    {
        map.ForMember(
            d => d.Id,
            opt => opt.MapFrom(s => s.Id.ToString())
        );
    }
}