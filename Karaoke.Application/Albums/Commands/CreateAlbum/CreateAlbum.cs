using FluentResults;
using Karaoke.Application.Common.Requests;
using MediatR;

namespace Karaoke.Application.Albums.Commands.CreateAlbum;

public static class CreateAlbum
{
    public class Command : IRequest<Result<string>>
    {
        public IEnumerable<LocalizedStringRequest> Titles { get; set; } = new List<LocalizedStringRequest>();

        public IEnumerable<string> Singers { get; set; } = new List<string>();

        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;
    }

    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IAlbumsService _albumsService;

        public Handler(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var album = await _albumsService.CreateAsync(request, cancellationToken);

            return Result.Ok(album.Id.ToString());
        }
    }
}