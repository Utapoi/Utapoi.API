using FluentResults;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongsForSinger;

public static partial class GetSongsForSinger
{
    internal sealed class Handler : IRequestHandler<Request, Result<List<Response>>>
    {
        private readonly ISongsService _songsService;

        public Handler(ISongsService songsService)
        {
            _songsService = songsService;
        }

        public async Task<Result<List<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var songs = await _songsService.GetForSingerAsync(request, cancellationToken);

                return Result.Ok(songs);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}