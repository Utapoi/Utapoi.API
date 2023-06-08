using FluentResults;
using Karaoke.Application.DTO;
using Karaoke.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Singers.Requests.SearchSingers;

public static class SearchSingers
{
    public record Request : IRequest<Result<IEnumerable<SingerDTO>>>
    {
        public string Input { get; init; } = string.Empty;
    }

    internal sealed class Handler : IRequestHandler<Request, Result<IEnumerable<SingerDTO>>>
    {
        private readonly IKaraokeDbContext _context;

        public Handler(IKaraokeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<SingerDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var singers = await _context.Singers
                .Include(x => x.Names)
                .ThenInclude(n => n.Language)
                .Where(s => s.Names.Any(
                    x => x.Text.Contains(request.Input, StringComparison.InvariantCultureIgnoreCase)))
                .Select(s => new SingerDTO
                {
                    Id = s.Id.ToString(),
                    Names = s.Names
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(singers.AsEnumerable());
        }
    }
}