using FluentResults;
using JetBrains.Annotations;
using MediatR;
using Utapoi.Application.Common.Errors;
using Utapoi.Core.Entities;
using Utapoi.Core.Exceptions;

namespace Utapoi.Application.Songs.Requests.GetSongForEdit;

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