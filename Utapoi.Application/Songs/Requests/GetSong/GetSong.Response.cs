using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;
using Utapoi.Core.Extensions;

namespace Utapoi.Application.Songs.Requests.GetSong;

public static partial class GetSong
{
    public struct SingerDTO : IProjection<Singer, SingerDTO>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public SingerDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Singer, SingerDTO> projection)
        {
            projection.ForMember(
                d => d.Cover,
                opt => opt.MapFrom(s => s.Cover!.GetUrl())
            );
        }
    }

    public struct AlbumDTO : IProjection<Album, AlbumDTO>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public IReadOnlyCollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

        public AlbumDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Album, AlbumDTO> projection)
        {
            projection.ForMember(
                d => d.Cover,
                opt => opt.MapFrom(s => s.Cover!.GetUrl())
            );
        }
    }

    public sealed class Response : IProjection<Song, Response>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public string OriginalFile { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public IReadOnlyCollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

        public Response()
        {
        }

        public void ConfigureProjection(IProjectionExpression<Song, Response> projection)
        {
            projection.ForMember(
                d => d.OriginalFile,
                opt => opt.MapFrom(s => s.OriginalFile != null ? s.OriginalFile.GetUrl() : string.Empty)
            );

            projection.ForMember(
                d => d.Cover,
                opt => opt.MapFrom(s => s.Thumbnail != null ? s.Thumbnail.GetUrl() : string.Empty)
            );

            projection.ForMember(
                d => d.Cover,
                opt => opt.MapFrom(s => s.Albums.First().Cover!.GetUrl())
            );
        }
    }
}