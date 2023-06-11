using AutoMapper;
using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.DTO;
using MediatR;

namespace Karaoke.Application.Albums.Requests.GetAlbums;

/// <summary>
///     Represents the request for getting all <see cref="Album" />s.
/// </summary>
public static class GetAlbums
{
    /// <summary>
    ///     The request for getting all <see cref="Album" />s.
    /// </summary>
    public class Request : IRequest<Result<PaginatedResponse<AlbumDTO>>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        public Request(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public Request()
        {
        }

        /// <summary>
        ///     The number of items to skip.
        /// </summary>
        public int Skip { get; init; }

        /// <summary>
        ///     The number of items to take.
        /// </summary>
        public int Take { get; init; }
    }

    /// <summary>
    ///     The handler for the <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<AlbumDTO>>>
    {
        private readonly IAlbumsService _albumsService;

        private readonly IMapper _mapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        public Handler(IMapper mapper, IAlbumsService albumsService)
        {
            _mapper = mapper;
            _albumsService = albumsService;
        }

        /// <inheritdoc />
        public async Task<Result<PaginatedResponse<AlbumDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var albums = await _albumsService.GetAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<AlbumDTO>
            {
                Items = _mapper.Map<IEnumerable<AlbumDTO>>(albums),
                Count = albums.Count,
                TotalCount = await _albumsService.CountAsync(cancellationToken)
            });
        }
    }
}