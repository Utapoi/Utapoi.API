using FluentResults;
using Karaoke.Application.Common.Errors;
using MediatR;

namespace Karaoke.Application.Singers.Commands.DeleteSinger;

public static partial class DeleteSinger
{
    internal sealed class Handler : IRequestHandler<Command, Result<Response>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _singersService.DeleteAsync(request, cancellationToken);

            return result
                ? Result.Ok()
                : Result.Fail(new EntityNotFoundError("Singer not found", request.Id));
        }
    }
}