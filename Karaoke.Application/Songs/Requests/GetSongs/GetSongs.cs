using AutoMapper;
using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.DTO;
using Karaoke.Core.Entities;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSongs;

/// <summary>
///     Represents the request for getting all <see cref="Song" />s.
/// </summary>
public static class GetSongs
{
    /// <summary>
    ///     The request for getting all <see cref="Song" />s.
    /// </summary>
    public sealed class Request : IRequest<Result<PaginatedResponse<SongDTO>>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        /// <param name="skip">The number of items to skip.</param>
        /// <param name="take">The number of items to take.</param>
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
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<SongDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ISongsService _songsService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="songsService">The <see cref="ISongsService" />.</param>
        /// <param name="mapper">The <see cref="IMapper" />.</param>
        public Handler(ISongsService songsService, IMapper mapper)
        {
            _mapper = mapper;
            _songsService = songsService;
        }

        /// <inheritdoc />
        public async Task<Result<PaginatedResponse<SongDTO>>> Handle(
            Request request,
            CancellationToken cancellationToken
        )
        {
            var songs = await _songsService.GetSongsAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<SongDTO>
            {
                Items = _mapper.Map<IReadOnlyCollection<SongDTO>>(songs),
                Count = songs.Count,
                TotalCount = await _songsService.CountAsync(cancellationToken)
            });
        }
    }
}