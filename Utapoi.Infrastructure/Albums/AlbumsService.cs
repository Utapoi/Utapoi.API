using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Utapoi.Application.Albums;
using Utapoi.Application.Albums.Commands.CreateAlbum;
using Utapoi.Application.Albums.Requests.GetAlbumsForAdmin;
using Utapoi.Application.Files;
using Utapoi.Application.Persistence;
using Utapoi.Application.Singers;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Albums;

public class AlbumsService : IAlbumsService
{
    private readonly IUtapoiDbContext _context;

    private readonly ISingersService _singersService;

    private readonly IFilesService _filesService;

    private readonly IMapper _mapper;

    public AlbumsService(IUtapoiDbContext context, ISingersService singersService, IFilesService filesService, IMapper mapper)
    {
        _context = context;
        _singersService = singersService;
        _filesService = filesService;
        _mapper = mapper;
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
            ReleaseDate = command.ReleaseDate.ToUniversalTime(),
            Cover = command.CoverFile != null ? await _filesService.CreateAsync(command.CoverFile, cancellationToken) : null,
            Singers = command.Singers
                .Select(x => _singersService.GetById(Guid.Parse(x))!)
                .ToList(),
            Songs = new List<Song>()
        };

        await _context.Albums.AddAsync(album, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return album;
    }

    public async Task<IReadOnlyCollection<GetAlbumsForAdmin.Response>> GetForAdminAsync(
        GetAlbumsForAdmin.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Albums
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<GetAlbumsForAdmin.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    //public async Task<IReadOnlyCollection<AlbumDTO>> GetAsync(GetAlbumsForAdmin.Request request, CancellationToken cancellationToken = default)
    //{
    //    return await _context
    //        .Albums
    //        .Skip(request.Skip)
    //        .Take(request.Take)
    //        .ProjectTo<AlbumDTO>(_mapper.ConfigurationProvider)
    //        .AsNoTracking()
    //        .ToListAsync(cancellationToken);
    //}

    public async Task<IEnumerable<Album>> SearchAsync(string input, CancellationToken cancellationToken)
    {
        return await _context
            .Albums
            .Include(x => x.Titles)
            .Include(x => x.Cover)
            .Include(x => x.Singers)
            .Include(x => x.Songs)
            .Where(x => x.Titles.Any(y => y.Text.Contains(input)))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context
            .Albums
            .CountAsync(cancellationToken);
    }
}