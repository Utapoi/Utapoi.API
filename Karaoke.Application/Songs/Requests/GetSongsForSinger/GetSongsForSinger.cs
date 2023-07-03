using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongsForSinger;

public static partial class GetSongsForSinger
{
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
    {
        private readonly ISongsService _songsService;

        public Handler(ISongsService songsService)
        {
            _songsService = songsService;
        }

        public async Task<Result<PaginatedResponse<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var songs = await _songsService.GetForSingerAsync(request, cancellationToken);

                return Result.Ok(new PaginatedResponse<Response>
                {
                    Items = songs,
                    Count = songs.Count,
                    TotalCount = await _songsService.CountAsync(x => x.Id == request.SingerId, cancellationToken)
                });
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}