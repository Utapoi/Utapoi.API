using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Utapoi.Application.Albums;
using Utapoi.Application.Files;
using Utapoi.Application.Karaoke;
using Utapoi.Application.Lyrics;
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

    private readonly IKaraokeDbContext _context;

    private readonly IFilesService _filesService;

    private readonly IKaraokeService _karaokeService;

    private readonly ISingersService _singersService;

    private readonly ITagsService _tagsService;

    private readonly ILyricsService _lyricsService;

    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SongsService" /> class.
    /// </summary>
    /// <param name="context">The <see cref="IKaraokeDbContext" /> context.</param>
    /// <param name="singersService">The singers service.</param>
    /// <param name="albumsService">The albums service.</param>
    /// <param name="tagsService">The tags service.</param>
    /// <param name="filesService">The files service.</param>
    /// <param name="karaokeService">The karaoke service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="lyricsService">The lyrics service.</param>
    public SongsService(
        IKaraokeDbContext context,
        ISingersService singersService,
        IAlbumsService albumsService,
        ITagsService tagsService,
        IFilesService filesService,
        IKaraokeService karaokeService,
        IMapper mapper,
        ILyricsService lyricsService
    )
    {
        _context = context;
        _singersService = singersService;
        _albumsService = albumsService;
        _tagsService = tagsService;
        _filesService = filesService;
        _karaokeService = karaokeService;
        _mapper = mapper;
        _lyricsService = lyricsService;
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
            Karaoke = new List<KaraokeInfo>()
        };

        song.Lyrics = command.Lyrics
            .Select(x => _lyricsService.Create(x, song))
            .ToList();

        if (command.Thumbnail?.File.Length > 0)
        {
            song.Thumbnail = await _filesService.CreateAsync(command.Thumbnail, cancellationToken);
        }

        if (command.VoiceFile?.File.Length > 0)
        {
            song.Vocal = await _filesService.CreateAsync(command.VoiceFile, cancellationToken);
        }

        if (command.InstrumentalFile?.File.Length > 0)
        {
            song.Instrumental = await _filesService.CreateAsync(command.InstrumentalFile, cancellationToken);
        }

        if (command.PreviewFile?.File.Length > 0)
        {
            song.Preview = await _filesService.CreateAsync(command.PreviewFile, cancellationToken);
        }

        foreach (var karaokeFile in command.KaraokeFiles)
        {
            if (karaokeFile == null)
            {
                continue;
            }

            if (karaokeFile?.File.Length == 0)
            {
                continue;
            }

            song.Karaoke.Add(await _karaokeService.CreateAsync(karaokeFile!, song, cancellationToken));
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
            .Include(x => x.Karaoke)
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