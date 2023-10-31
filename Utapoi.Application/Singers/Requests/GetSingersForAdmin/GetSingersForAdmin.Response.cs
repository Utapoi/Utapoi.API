using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;
using Utapoi.Core.Extensions;

namespace Utapoi.Application.Singers.Requests.GetSingersForAdmin;

public static partial class GetSingersForAdmin
{
    public struct AlbumDTO : IMap<Album, AlbumDTO>
    {
        public string Id { get; set; } = string.Empty;

        public AlbumDTO()
        {
        }

        public readonly void ConfigureMapping(IProjectionExpression<Album, AlbumDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    public struct SongDTO : IMap<Song, SongDTO>
    {
        public string Id { get; set; } = string.Empty;

        public SongDTO()
        {
        }

        public readonly void ConfigureMapping(IProjectionExpression<Song, SongDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    public sealed class Response : IMap<Singer, Response>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public string ProfilePicture { get; set; } = string.Empty;

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public IReadOnlyCollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        public void ConfigureMapping(IProjectionExpression<Singer, Response> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );

            projection.ForMember(
                d => d.ProfilePicture,
                opt => opt.MapFrom(s => s.ProfilePicture.GetUrl())
            );
        }
    }
}
