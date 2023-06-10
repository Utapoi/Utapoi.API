using Karaoke.Application.Files;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyCollection<Singer>> GetAsync(
        GetSingers.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Singers
            .Include(x => x.Names)
            .Include(x => x.ProfilePicture)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context.Singers.CountAsync(cancellationToken);
    }
}