using Karaoke.Application.Albums;
using Karaoke.Application.Files;
using Karaoke.Application.Karaoke;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Application.Songs;
using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Tags;
using Karaoke.Core.Entities;

namespace Karaoke.Infrastructure.Songs;

public sealed class SongsService : ISongsService
{
    private readonly IAlbumsService _albumsService;

    private readonly IKaraokeDbContext _context;

    private readonly IFilesService _filesService;

    private readonly IKaraokeService _karaokeService;

    private readonly ISingersService _singersService;

    private readonly ITagsService _tagsService;

    public SongsService(
        IKaraokeDbContext context,
        ISingersService singersService,
        IAlbumsService albumsService,
        ITagsService tagsService,
        IFilesService filesService,
        IKaraokeService karaokeService
    )
    {
        _context = context;
        _singersService = singersService;
        _albumsService = albumsService;
        _tagsService = tagsService;
        _filesService = filesService;
        _karaokeService = karaokeService;
    }

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
}