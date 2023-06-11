using AutoMapper;
using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.DTO;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSingers;

/// <summary>
///     Represents a request to get singers.
/// </summary>
public static class GetSingers
{
    /// <summary>
    ///     Represents a request to get a paginated list of singers.
    /// </summary>
    public sealed class Request : IRequest<Result<PaginatedResponse<SingerDTO>>>
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
        ///     Gets the number of items to skip.
        /// </summary>
        public int Skip { get; init; }

        /// <summary>
        ///     Gets the number of items to take.
        /// </summary>
        public int Take { get; init; }
    }

    /// <summary>
    ///     Represents a handler for <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<SingerDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ISingersService _singersService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="singersService">The <see cref="ISingersService" />.</param>
        /// <param name="mapper">The <see cref="IMapper" />.</param>
        public Handler(ISingersService singersService, IMapper mapper)
        {
            _singersService = singersService;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<SingerDTO>>> Handle(
            Request request,
            CancellationToken cancellationToken
        )
        {
            var singers = await _singersService.GetAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<SingerDTO>
            {
                Items = _mapper.Map<IEnumerable<SingerDTO>>(singers),
                Count = singers.Count,
                TotalCount = await _singersService.CountAsync(cancellationToken)
            });
        }
    }
}