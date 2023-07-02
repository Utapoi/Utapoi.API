using AutoMapper;
using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;
using Karaoke.Core.Extensions;

namespace Karaoke.Application.Songs.Requests.GetSong;

public static partial class GetSong
{
    public struct SingerDTO : IProjection<Singer, SingerDTO>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public SingerDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Singer, SingerDTO> projection)
        {
        }
    }

    public sealed class Response : IProjection<Song, Response>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public string OriginalFile { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

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