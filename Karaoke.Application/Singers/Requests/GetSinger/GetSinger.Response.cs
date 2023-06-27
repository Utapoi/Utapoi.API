using AutoMapper;
using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;
using Karaoke.Core.Extensions;

namespace Karaoke.Application.Singers.Requests.GetSinger
{
    public static partial class GetSinger
    {
        public struct AlbumDTO : IProjection<Album, AlbumDTO>
        {
            public string Id { get; set; } = string.Empty;

            public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

            public string Cover { get; set; } = string.Empty;

            public AlbumDTO()
            {
            }

            public readonly void ConfigureProjection(IProjectionExpression<Album, AlbumDTO> projection)
            {
                projection.ForMember(
                    d => d.Id,
                    opt => opt.MapFrom(s => s.Id.ToString())
                );

                projection.ForMember(
                    d => d.Cover,
                    opt => opt.MapFrom(s => s.Cover!.GetUrl())
                );
            }
        }

        public sealed class Response : IProjection<Singer, Response>
        {
            public Guid Id { get; set; } = Guid.Empty;

            public IReadOnlyCollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

            public IReadOnlyCollection<LocalizedString> Nicknames { get; set; } = new List<LocalizedString>();

            public string ProfilePicture { get; set; } = string.Empty;

            public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

            public int AlbumsCount { get; set; }

            public int SongsCount { get; set; }

            public void ConfigureProjection(IProjectionExpression<Singer, Response> projection)
            {
                projection.ForMember(
                    d => d.ProfilePicture,
                    opt => opt.MapFrom(s => s.ProfilePicture.GetUrl())
                );

                projection.ForMember(
                    d => d.AlbumsCount,
                    opt => opt.MapFrom(s => s.Albums.Count)
                );

                projection.ForMember(
                    d => d.SongsCount,
                    opt => opt.MapFrom(s => s.Songs.Count)
                );
            }
        }
    }
}
