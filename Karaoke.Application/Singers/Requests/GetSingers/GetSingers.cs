using AutoMapper;
using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.DTO;
using Karaoke.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Singers.Requests.GetSingers;

public static class GetSingers
{
    public sealed record Request : IRequest<Result<PaginatedResponse<SingerDTO>>>
    {
        public int Skip { get; init; }

        public int Take { get; init; }
    }

    internal sealed class Handler : IRequestHandler<Request, Result<PaginatedResponse<SingerDTO>>>
    {
        private readonly IKaraokeDbContext _context;

        private readonly IMapper _mapper;

        public Handler(IKaraokeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResponse<SingerDTO>>> Handle(Request request,
            CancellationToken cancellationToken)
        {
            var singers = await _context.Singers
                .Include(x => x.Names)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

            return Result.Ok(new PaginatedResponse<SingerDTO>
            {
                Items = _mapper.Map<IEnumerable<SingerDTO>>(singers),
                Count = singers.Count,
                TotalCount = await _context.Singers.CountAsync(cancellationToken)
            });
        }
    }
}