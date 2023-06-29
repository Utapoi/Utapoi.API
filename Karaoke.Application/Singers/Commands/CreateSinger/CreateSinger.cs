using FluentResults;
using MediatR;

namespace Karaoke.Application.Singers.Commands.CreateSinger;

public static partial class CreateSinger
{
    /// <summary>
    /// The handler of the command to create a singer.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Command, Result<Response>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var response = await _singersService.CreateAsync(request, cancellationToken);

            return Result.Ok(response);
        }
    }
}