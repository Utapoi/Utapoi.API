using Karaoke.Application.Files;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Core.Entities;

namespace Karaoke.Infrastructure.Singers;

public class SingersService : ISingersService
{
    private readonly IKaraokeDbContext _context;

    private readonly IFilesService _filesService;

    public SingersService(IKaraokeDbContext context, IFilesService filesService)
    {
        _context = context;
        _filesService = filesService;
    }

    public Singer? GetById(Guid id)
    {
        return _context
            .Singers
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<Singer> CreateAsync(CreateSinger.Command command, CancellationToken cancellationToken)
    {
        var singer = new Singer
        {
            Names = command.Names
                .Select(x => new LocalizedString
                {
                    Text = x.Text,
                    Language = x.Language
                }).ToList(),
            Nicknames = command.Nicknames
                .Select(x => new LocalizedString
                {
                    Text = x.Text,
                    Language = x.Language
                }).ToList(),
            Birthday = command.Birthday ?? DateTime.MinValue,
            ProfilePicture = await _filesService.CreateAsync(command.Image, cancellationToken)
        };

        await _context.Singers.AddAsync(singer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return singer;
    }
}