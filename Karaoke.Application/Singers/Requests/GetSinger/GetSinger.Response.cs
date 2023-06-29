using AutoMapper;
using Karaoke.Application.Common.Mappings;
using Karaoke.Core.Entities;
using Karaoke.Core.Extensions;

namespace Karaoke.Application.Singers.Requests.GetSinger;

// Note(Mikyan): This is more of a global concern, but I'll write it here.
// I don't know if this is the best solution but at least we have separated objects for each request.
// I think this is better than having a single object with a lot of properties that are not used.
// But this may not be the best implementation.
// Using AutoMapper's projection may have some constraints in the future.

public static partial class GetSinger
{
    public struct SongDTO : IProjection<Song, SongDTO>, IProjection<Song?, SongDTO?>
    {
        public Guid Id { get; set; } = Guid.Empty;

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public SongDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Song, SongDTO> projection)
        {
            projection.ForMember(
                d => d.Url,
                opt => opt.MapFrom(s => s.OriginalFile != null ? s.OriginalFile.GetUrl() : string.Empty)
            );

            projection.ForMember(
                d => d.Cover,
                opt => opt.MapFrom(s => s.Albums.First(x => x.Cover != null).Cover!.GetUrl())
            );;
        }
    }

    public struct AlbumDTO : IProjection<Album, AlbumDTO>
    {
        public Guid Id { get; set; } = Guid.Empty;

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

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

    public sealed class Response : IProjection<Singer, Response>
    {
        public Guid Id { get; set; } = Guid.Empty;

        public IReadOnlyCollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public IReadOnlyCollection<LocalizedString> Nicknames { get; set; } = new List<LocalizedString>();

        public IReadOnlyCollection<LocalizedString> Descriptions { get; set; } = new List<LocalizedString>();

        public IReadOnlyCollection<LocalizedString> Activities { get; set; } = new List<LocalizedString>();

        public DateTime Birthday { get; set; }

        public string BloodType { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public float Height { get; set; }

        public string ProfilePicture { get; set; } = string.Empty;

        public SongDTO? PopularSong { get; set; }

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

        public int AlbumsCount { get; set; }

        public int SongsCount { get; set; }

        public void ConfigureProjection(IProjectionExpression<Singer, Response> projection)
        {
            projection.ForMember(
                d => d.Names,
                opt => opt.MapFrom(s => s.Names.OrderBy(x => x.Text))
            );

            projection.ForMember(
                d => d.Nicknames,
                opt => opt.MapFrom(s => s.Nicknames.OrderBy(x => x.Text))
            );

            projection.ForMember(
                d => d.ProfilePicture,
                opt => opt.MapFrom(s => s.ProfilePicture.GetUrl())
            );

            projection.ForMember(
                d => d.Albums,
                opt => opt.MapFrom(s => s.Albums.OrderBy(x => x.ReleaseDate))
            );

            projection.ForMember(
                d => d.PopularSong,
                opt => opt.MapFrom(
                    s => s.Songs.FirstOrDefault()
                )
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
