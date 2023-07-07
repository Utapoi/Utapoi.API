using FluentResults;
using JetBrains.Annotations;
using Karaoke.Application.Common.Errors;
using Karaoke.Core.Entities;
using Karaoke.Core.Exceptions;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongForEdit;

public static partial class GetSongForEdit
{
    [UsedImplicitly]
    internal sealed class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly ISongsService _songsService;

        public Handler(ISongsService songsService)
        {
            _songsService = songsService;
        }

        public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var song = await _songsService.GetForEditAsync(request, cancellationToken);

                return Result.Ok(song);
            }
            catch (EntityNotFoundException<Song> ex)
            {
                return Result.Fail(new EntityNotFoundError(ex.Message, request.SongId));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}