using AutoMapper;
using AutoMapper.QueryableExtensions;
using Karaoke.Application.Albums;
using Karaoke.Application.Files;
using Karaoke.Application.Karaoke;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Application.Songs;
using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Songs.Requests.GetSongsForAdmin;
using Karaoke.Application.Tags;
using Karaoke.Core.Entities;
using Karaoke.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Songs;

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
    public SongsService(
        IKaraokeDbContext context,
        ISingersService singersService,
        IAlbumsService albumsService,
        ITagsService tagsService,
        IFilesService filesService,
        IKaraokeService karaokeService
,
        IMapper mapper)
    {
        _context = context;
        _singersService = singersService;
        _albumsService = albumsService;
        _tagsService = tagsService;
        _filesService = filesService;
        _karaokeService = karaokeService;
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
            ReleaseDate = command.ReleaseDate,
            Thumbnail = await _filesService.CreateAsync(command.Thumbnail, cancellationToken),
            Vocal = await _filesService.CreateAsync(command.VoiceFile, cancellationToken),
            Instrumental = await _filesService.CreateAsync(command.InstrumentalFile, cancellationToken),
            Preview = await _filesService.CreateAsync(command.PreviewFile, cancellationToken),
            Karaoke = new List<KaraokeInfo>()
        };

        foreach (var karaokeFile in command.KaraokeFiles)
        {
            song.Karaoke.Add(await _karaokeService.CreateAsync(karaokeFile, song, cancellationToken));
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

    /// <inheritdoc cref="ISongsService.GetAsync(Guid, CancellationToken)" />
    public async Task<Song> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var song = await _context.Songs
            .Include(x => x.Singers)
            .Include(x => x.Albums)
            .Include(x => x.Tags)
            .Include(x => x.Karaoke)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return song ?? throw new EntityNotFoundException<Song>(id);
    }

    /// <inheritdoc cref="ISongsService.CountAsync(CancellationToken)" />
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context.Songs
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }
}