using AutoMapper;
using Karaoke.Application.DTO;
using Karaoke.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Application.Songs.Requests.GetSongs;

public static class GetSongs
{
    public sealed class Request : IRequest<Response>
    {
    }

    public sealed class Response
    {
        public IEnumerable<SongDTO> Songs { get; set; } = Enumerable.Empty<SongDTO>();
    }

    internal sealed class Handler : IRequestHandler<Request, Response>
    {
        private readonly IKaraokeDbContext _context;

        private readonly IMapper _mapper;

        public Handler(IKaraokeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var songs = await _context
                .Songs
                .ToListAsync(cancellationToken);

            return new Response
            {
                Songs = _mapper.Map<IEnumerable<SongDTO>>(songs)
            };
        }
    }
}