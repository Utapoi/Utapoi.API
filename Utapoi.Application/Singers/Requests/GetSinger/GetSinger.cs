using AutoMapper;
using FluentResults;
using MediatR;
using Utapoi.Application.Common.Errors;

namespace Utapoi.Application.Singers.Requests.GetSinger;

public static partial class GetSinger
{
    internal sealed class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly ISingersService _singersService;

        private readonly IMapper _mapper;

        public Handler(ISingersService singersService, IMapper mapper)
        {
            _singersService = singersService;
            _mapper = mapper;
        }

        public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singer = await _singersService.GetByIdAsync(request.Id, cancellationToken);

            return singer is null
                ? Result.Fail(new EntityNotFoundError("Singer not found", request.Id))
                : Result.Ok(_mapper.Map<Response>(singer));
        }
    }
}
