using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Utapoi.Application.Albums;
using Utapoi.Application.Files;
using Utapoi.Application.Persistence;
using Utapoi.Application.Singers;
using Utapoi.Application.Songs;
using Utapoi.Application.Songs.Commands.CreateSong;
using Utapoi.Application.Songs.Requests.GetSong;
using Utapoi.Application.Songs.Requests.GetSongForEdit;
using Utapoi.Application.Songs.Requests.GetSongsForAdmin;
using Utapoi.Application.Songs.Requests.GetSongsForSinger;
using Utapoi.Application.Tags;
using Utapoi.Core.Entities;
using Utapoi.Core.Exceptions;

namespace Utapoi.Infrastructure.Songs;

/// <summary>
///     The <see cref="SongsService" /> class that implements <see cref="ISongsService" />.
/// </summary>
public sealed class SongsService : ISongsService
{
    private readonly IAlbumsService _albumsService;

    private readonly IUtapoiDbContext _context;

    private readonly IFilesService _filesService;

    private readonly ISingersService _singersService;

    private readonly ITagsService _tagsService;

    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SongsService" /> class.
    /// </summary>
    /// <param name="context">The <see cref="IUtapoiDbContext" /> context.</param>
    /// <param name="singersService">The singers service.</param>
    /// <param name="albumsService">The albums service.</param>
    /// <param name="tagsService">The tags service.</param>
    /// <param name="filesService">The files service.</param>
    /// <param name="karaokeService">The karaoke service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="lyricsService">The lyrics service.</param>
    public SongsService(
        IUtapoiDbContext context,
        ISingersService singersService,
        IAlbumsService albumsService,
        ITagsService tagsService,
        IFilesService filesService,
        IMapper mapper
    )
    {
        _context = context;
        _singersService = singersService;
        _albumsService = albumsService;
        _tagsService = tagsService;
        _filesService = filesService;
        _mapper = mapper;
    }

    /// <inheritdoc cref="ISongsService.CreateAsync(CreateSong.Command, CancellationToken)" />
    public async Task<string> CreateAsync(CreateSong.Command command, CancellationToken cancellationToken = default)
    {
        var song = new Song
        {
            Titles = command.Titles
                .Select(x => new LocalizedString
                {
                    Text = x.Text,
                    Language = x.Language
                }).ToList(),
            Singers = command.Singers
                .Select(x => _singersService.GetById(Guid.Parse(x))!) // Should never be null in this case.
                .ToList(),
            Albums = command.Albums
                .Select(x => _albumsService.GetById(Guid.Parse(x))!) // Should never be null in this case.
                .ToList(),
            Tags = command.Tags
                .Select(x => _tagsService.GetOrCreateByName(x))
                .ToList(),
            ReleaseDate = command.ReleaseDate.ToUniversalTime(),
        };


        if (command.Thumbnail?.File.Length > 0)
        {
            song.Thumbnail = await _filesService.CreateAsync(command.Thumbnail, cancellationToken);
        }

        if (command.SongFile?.File.Length > 0)
        {
            song.SongFile = await _filesService.CreateAsync(command.SongFile, cancellationToken);
        }

        await _context.Songs.AddAsync(song, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return song.Id.ToString();
    }

    /// <inheritdoc cref="ISongsService.GetForAdminAsync(GetSongsForAdmin.Request, CancellationToken)" />
    public async Task<IReadOnlyCollection<GetSongsForAdmin.Response>> GetForAdminAsync(
        GetSongsForAdmin.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Songs
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<GetSongsForAdmin.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<GetSongForEdit.Response> GetForEditAsync(
        GetSongForEdit.Request request,
        CancellationToken cancellationToken = default
    )
    {
        var song = await _context.Songs
            .ProjectTo<GetSongForEdit.Response>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.SongId, cancellationToken);

        return song ?? throw new EntityNotFoundException<Song>(request.SongId);
    }

    /// <inheritdoc cref="ISongsService.GetAsync(Guid, CancellationToken)" />
    public async Task<GetSong.Response> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var song = await _context.Songs
            .Include(x => x.Singers)
            .Include(x => x.Albums)
            .Include(x => x.Tags)
            .ProjectTo<GetSong.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return song ?? throw new EntityNotFoundException<Song>(id);
    }

    /// <inheritdoc cref="ISongsService.GetForSingerAsync(GetSongsForSinger.Request, CancellationToken)" />
    public async Task<IReadOnlyCollection<GetSongsForSinger.Response>> GetForSingerAsync(GetSongsForSinger.Request request, CancellationToken cancellationToken = default)
    {
        return await _context.Songs
            .Include(x => x.Singers)
            .Include(x => x.Albums)
            .Where(x => x.Singers.Any(s => s.Id == request.SingerId))
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<GetSongsForSinger.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc cref="ISongsService.CountAsync(CancellationToken)" />
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context
            .Songs
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    /// <inheritdoc cref="ISongsService.CountAsync(Expression{Func{Song,bool}}, CancellationToken)" />
    public Task<int> CountAsync(Expression<Func<Song, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _context
            .Songs
            .Where(predicate)
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }
}