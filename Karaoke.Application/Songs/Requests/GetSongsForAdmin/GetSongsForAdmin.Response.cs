using AutoMapper;
using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Songs.Requests.GetSongsForAdmin;

public static partial class GetSongsForAdmin
{
    public struct SingerDTO : IProjection<Singer, SingerDTO>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public SingerDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Singer, SingerDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    public struct AlbumDTO : IProjection<Album, AlbumDTO>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public AlbumDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Album, AlbumDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    public sealed class Response : IProjection<Song, Response>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public IReadOnlyCollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public void ConfigureProjection(IProjectionExpression<Song, Response> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }
}
