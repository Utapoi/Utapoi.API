using FluentResults;
using Karaoke.Application.DTO;
using Karaoke.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Singers.Requests.SearchSingers;

public static class SearchSingers
{
    public sealed class Request : IRequest<Result<List<SingerDTO>>>
    {
        public string Input { get; init; } = string.Empty;
    }

    internal sealed class Handler : IRequestHandler<Request, Result<List<SingerDTO>>>
    {
        private readonly ISingersService _singersService;

        public Handler(ISingersService singersService)
        {
            _singersService = singersService;
        }

        public async Task<Result<List<SingerDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singers = await _singersService.SearchAsync(request, cancellationToken);

            return Result.Ok(singers.Select(x => new SingerDTO
            {
                Id = x.Id.ToString(),
                Names = x.Names,
            }).ToList());
        }
    }
}