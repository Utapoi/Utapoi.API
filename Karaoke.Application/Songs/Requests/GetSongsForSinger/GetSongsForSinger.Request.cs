using FluentResults;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongsForSinger;

public static partial class GetSongsForSinger
{
    public sealed class Request : IRequest<Result<List<Response>>>
    {
        public Guid SingerId { get; set; } = Guid.Empty;

        public Request(Guid singerId)
        {
            SingerId = singerId;
        }

        public Request()
        {
        }
    }
}