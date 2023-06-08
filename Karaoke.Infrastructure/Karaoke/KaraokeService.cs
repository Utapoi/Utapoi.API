using Karaoke.Application.Common.Requests;
using Karaoke.Application.Files;
using Karaoke.Application.Karaoke;
using Karaoke.Application.Persistence;
using Karaoke.Core.Entities;

namespace Karaoke.Infrastructure.Karaoke;

public class KaraokeService : IKaraokeService
{
    private readonly IKaraokeDbContext _context;

    private readonly IFilesService _filesService;

    public KaraokeService(IKaraokeDbContext context, IFilesService filesService)
    {
        _context = context;
        _filesService = filesService;
    }

    public async Task<KaraokeInfo> CreateAsync(
        LocalizedFileRequest request,
        Song song,
        CancellationToken cancellationToken = default
    )
    {
        var karaoke = new KaraokeInfo
        {
            Song = song,
            Language = request.Language,
            File = await CreateKaraokeFileAsync(request, cancellationToken)
        };

        await _context.KaraokeInfos.AddAsync(karaoke, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return karaoke;
    }

    private Task<NamedFile> CreateKaraokeFileAsync(
        LocalizedFileRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return _filesService.CreateAsync(request, cancellationToken);
    }
}