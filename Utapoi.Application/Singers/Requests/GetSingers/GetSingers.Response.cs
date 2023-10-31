using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;
using Utapoi.Core.Extensions;

namespace Utapoi.Application.Singers.Requests.GetSingers;

public static partial class GetSingers
{
    public sealed class Response : IMap<Singer, Response>
    {
        public string Id { get; set; } = string.Empty;

        public IList<LocalizedString> Names { get; set; } = new List<LocalizedString>();

        public string ProfilePicture { get; set; } = string.Empty;

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
