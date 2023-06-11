using Karaoke.Application.Albums;
using Karaoke.Application.Albums.Commands.CreateAlbum;
using Karaoke.Application.Albums.Requests.GetAlbums;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Albums;

public class AlbumsService : IAlbumsService
{
    private readonly IKaraokeDbContext _context;

    private readonly ISingersService _singersService;

    public AlbumsService(IKaraokeDbContext context, ISingersService singersService)
    {
        _context = context;
        _singersService = singersService;
    }

    public Album? GetById(Guid id)
    {
        return _context
            .Albums
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Albums
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Album> CreateAsync(CreateAlbum.Command command, CancellationToken cancellationToken)
    {
        var album = new Album
        {
            Titles = command.Titles
                .Select(x => new LocalizedString
                {
                    Text = x.Text,
                    Language = x.Language
                }).ToList(),
            ReleaseDate = command.ReleaseDate,
            Singers = command.Singers
                .Select(x => _singersService.GetById(Guid.Parse(x))!)
                .ToList(),
            Songs = new List<Song>()
        };

        await _context.Albums.AddAsync(album, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return album;
    }

    public async Task<IReadOnlyCollection<Album>> GetAsync(GetAlbums.Request request, CancellationToken cancellationToken = default)
    {
        // Note(Mikyan): This is a very bad idea, but I'm too lazy to implement a proper pagination.
        // todo fix this.
        return await _context
            .Albums
            .Include(x => x.Titles)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Album>> SearchAsync(string input, CancellationToken cancellationToken)
    {
        // Note(Mikyan): This is a very bad idea, but I'm too lazy to implement a proper pagination.
        // todo fix this.
        return await _context
            .Albums
            .Where(x => x.Titles.Any(y => y.Text.Contains(input)))
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context
            .Albums
            .CountAsync(cancellationToken);
    }
}