using AutoMapper;
using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;
using Karaoke.Core.Extensions;

namespace Karaoke.Application.Singers.Requests.GetSingersForAdmin;

public static partial class GetSingersForAdmin
{
    public struct AlbumDTO : IProjection<Album, AlbumDTO>
    {
        public string Id { get; set; } = string.Empty;

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

    public struct SongDTO : IProjection<Song, SongDTO>
    {
        public string Id { get; set; } = string.Empty;

        public SongDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Song, SongDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    public sealed class Response : IProjection<Singer, Response>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public string ProfilePicture { get; set; } = string.Empty;

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public IReadOnlyCollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        public void ConfigureProjection(IProjectionExpression<Singer, Response> projection)
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
