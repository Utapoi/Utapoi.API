using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSingers;

public static partial class GetSingers
{
    public sealed class Request : IRequest<Result<PaginatedResponse<Response>>>
    {
        public int Skip { get; init; }

        public int Take { get; init; }

        public Request()
        {
        }

        public Request(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

    }
}
