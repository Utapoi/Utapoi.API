using FluentResults;
using MediatR;

namespace Utapoi.Application.Songs.Requests.GetSongForEdit;

public static partial class GetSongForEdit
{
    public sealed class Request : IRequest<Result<Response>>
    {
        public Guid SongId { get; set; } = Guid.Empty;

        public Request()
        {
        }

        public Request(Guid id)
        {
            SongId = id;
        }
    }
}