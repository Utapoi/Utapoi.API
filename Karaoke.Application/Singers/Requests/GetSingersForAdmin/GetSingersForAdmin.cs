using AutoMapper;
using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.DTO;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSingersForAdmin;

/// <summary>
///     Represents a request to get singers.
/// </summary>
public static partial class GetSingersForAdmin
{
    /// <summary>
    ///     Represents a handler for <see cref="Request" />.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
    {
        private readonly ISingersService _singersService;

        private readonly IMapper _mapper;

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

        public async Task<Result<PaginatedResponse<Response>>> Handle(
            Request request,
            CancellationToken cancellationToken
        )
        {
            var singers = await _singersService.GetForAdminAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<Response>
            {
                Items = _mapper.Map<List<Response>>(singers),
                Count = singers.Count,
                TotalCount = await _singersService.CountAsync(cancellationToken)
            });
        }
    }
}