using FluentResults;
using MediatR;

namespace Karaoke.Application.Singers.Commands.EditSinger;

public static partial class EditSinger
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
            var response = await _singersService.EditAsync(request, cancellationToken);

            // Note(Mikyan): Add a specific error code for this error?

            return response == null
                ? Result.Fail("Cannot update the singer.")
                : Result.Ok(response);
        }
    }
}