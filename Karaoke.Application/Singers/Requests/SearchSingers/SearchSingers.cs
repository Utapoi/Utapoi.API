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
        private readonly IKaraokeDbContext _context;

        public Handler(IKaraokeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<SingerDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singers = await _context.Singers
                .Include(x => x.Names)
                .Where(s => s.Names.Any(
                    x => x.Text.Contains(request.Input))
                )
                .Select(s => new SingerDTO
                {
                    Id = s.Id.ToString(),
                    Names = s.Names
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(singers);
        }
    }
}