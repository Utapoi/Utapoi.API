using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongsForSinger;

public static partial class GetSongsForSinger
{
    public sealed class Request : IRequest<Result<PaginatedResponse<Response>>>
    {
        public Guid SingerId { get; init; } = Guid.Empty;

        public int Skip { get; init; }

        public int Take { get; init; }

        public Request(Guid singerId)
        {
            SingerId = singerId;
        }

        public Request()
        {
        }
    }
}