using AutoMapper;
using FluentResults;
using MediatR;
using Utapoi.Application.Common;

namespace Utapoi.Application.Singers.Requests.GetSingers;

public static partial class GetSingers
{
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
    {
        private readonly ISingersService _singersService;

        private readonly IMapper _mapper;

        public Handler(ISingersService singersService, IMapper mapper)
        {
            _singersService = singersService;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singers = await _singersService.GetAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<Response>
            {
                Items = _mapper.Map<List<Response>>(singers),
                Count = singers.Count,
                TotalCount = await _singersService.CountAsync(cancellationToken)
            });
        }
    }
}
