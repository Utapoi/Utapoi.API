using FluentResults;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSinger;

public static partial class GetSinger
{
    public sealed class Request : IRequest<Result<Response>>
    {
        public Request(Guid id)
        {
            Id = id;
        }

        public Request()
        {
        }

        public Guid Id { get; init; } = Guid.Empty;
    }   
}
