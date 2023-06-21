using FluentResults;
using Karaoke.Application.Common.Requests;
using MediatR;

namespace Karaoke.Application.Singers.Commands.CreateSinger;

public static class CreateSinger
{
    public record Command : IRequest<Result<string>>
    {
        public IEnumerable<LocalizedStringRequest> Names { get; set; } = new List<LocalizedStringRequest>();

        public IEnumerable<LocalizedStringRequest> Nicknames { get; set; } = new List<LocalizedStringRequest>();

        public DateTime? Birthday { get; set; } = DateTime.MinValue;

        public FileRequest ProfilePictureFile { get; set; } = null!;
    }

    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var singer = await _singersService.CreateAsync(request, cancellationToken);

            return Result.Ok(singer.Id.ToString());
        }
    }
}