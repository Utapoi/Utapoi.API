using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSingers;

public static partial class GetSingers
{
    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<Response>>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<PaginatedResponse<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singers = await _singersService.GetAsync(request, cancellationToken);

            return Result.Ok(new PaginatedResponse<Response>
            {
                Items = singers,
                Count = singers.Count,
                TotalCount = await _singersService.CountAsync(cancellationToken)
            });
        }
    }
}
