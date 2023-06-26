using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Albums.Requests.GetAlbumsForAdmin;

/// <summary>
///     Represents the request for getting all <see cref="Album" />s.
/// </summary>
public static partial class GetAlbumsForAdmin
{
    /// <summary>
    ///     The handler for the <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
    {
        private readonly IAlbumsService _albumsService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        public Handler(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        /// <inheritdoc />
        public async Task<Result<PaginatedResponse<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var albums = await _albumsService.GetForAdminAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<Response>
            {
                Items = albums,
                Count = albums.Count,
                TotalCount = await _albumsService.CountAsync(cancellationToken)
            });
        }
    }
}