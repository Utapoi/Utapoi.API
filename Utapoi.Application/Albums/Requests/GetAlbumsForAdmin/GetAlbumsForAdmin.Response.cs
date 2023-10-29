using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Albums.Requests.GetAlbumsForAdmin;

public static partial class GetAlbumsForAdmin
{
    /// <summary>
    /// Internal DTO for <see cref="Singer" /> used only for this response.
    /// </summary>
    public struct SingerDTO : IProjection<Singer, SingerDTO>
    {
        public IList<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public SingerDTO()
        {
        }
    }

    /// <summary>
    /// Internal DTO for <see cref="Song" /> used only for this response.
    /// </summary>
    public struct SongDTO : IProjection<Song, SongDTO>
    {
        public string Id { get; set; } = string.Empty;

        public SongDTO()
        {
        }

        /// <summary>
        ///  Configures the projection between <see cref="Song" /> and <see cref="SongDTO" />.
        /// </summary>
        /// <param name="projection">The projection to configure.</param>
        public readonly void ConfigureProjection(IProjectionExpression<Song, SongDTO> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }

    /// <summary>
    /// Response for a <see cref="GetAlbumsForAdmin.Request"/> request.
    /// </summary>
    public sealed class Response : IProjection<Album, Response>
    {
        public string Id { get; set; } = string.Empty;

        public IEnumerable<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

        public IReadOnlyCollection<SingerDTO> Singers { get; set; } = new List<SingerDTO>();

        public IReadOnlyCollection<SongDTO> Songs { get; set; } = new List<SongDTO>();

        /// <summary>
        ///   Configures the projection between <see cref="Album" /> and <see cref="Response" />.
        /// </summary>
        /// <param name="projection">The projection to configure.</param>
        public void ConfigureProjection(IProjectionExpression<Album, Response> projection)
        {
            projection.ForMember(
                d => d.Id,
                opt => opt.MapFrom(s => s.Id.ToString())
            );
        }
    }
}
