using FluentResults;
using Karaoke.Application.Common.Errors;
using MediatR;

namespace Karaoke.Application.Singers.Requests.GetSinger;

public static partial class GetSinger
{
    internal sealed class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singer = await _singersService.GetByIdAsync(request.Id, cancellationToken);

            return singer is null
                ? Result.Fail(new EntityNotFoundError("Singer not found", request.Id))
                : Result.Ok(singer);
        }
    }
}
