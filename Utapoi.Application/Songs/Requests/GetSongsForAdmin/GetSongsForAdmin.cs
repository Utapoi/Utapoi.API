using AutoMapper;
using FluentResults;
using MediatR;
using Utapoi.Application.Common;

namespace Utapoi.Application.Songs.Requests.GetSongsForAdmin;

/// <summary>
///     Represents the request for getting all <see cref="Song" />s.
/// </summary>
public static partial class GetSongsForAdmin
{
    /// <summary>
    ///     The handler for the <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
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
        public async Task<Result<PaginatedResponse<Response>>> Handle(
            Request request,
            CancellationToken cancellationToken
        )
        {
            var songs = await _songsService.GetForAdminAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<Response>
            {
                Items = songs,
                Count = songs.Count,
                TotalCount = await _songsService.CountAsync(cancellationToken)
            });
        }
    }
}