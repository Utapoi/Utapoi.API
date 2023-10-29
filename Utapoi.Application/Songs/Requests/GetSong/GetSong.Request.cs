using FluentResults;
using MediatR;

namespace Utapoi.Application.Songs.Requests.GetSong;

public static partial class GetSong
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

        public Guid Id { get; set; } = Guid.Empty;
    }
}