using AutoMapper;
using AutoMapper.QueryableExtensions;
using Karaoke.Application.Files;
using Karaoke.Application.LocalizedStrings.Interfaces;
using Karaoke.Application.Persistence;
using Karaoke.Application.Singers;
using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Commands.DeleteSinger;
using Karaoke.Application.Singers.Commands.EditSinger;
using Karaoke.Application.Singers.Requests.GetSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Karaoke.Application.Singers.Requests.SearchSingers;
using Karaoke.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Singers;

public class SingersService : ISingersService
{
    private readonly IKaraokeDbContext _context;

    private readonly IFilesService _filesService;

    private readonly ILocalizedStringsService _localizedStringsService;

    private readonly IMapper _mapper;

    public SingersService(IKaraokeDbContext context, IFilesService filesService, IMapper mapper, ILocalizedStringsService localizedStringsService)
    {
        _context = context;
        _filesService = filesService;
        _mapper = mapper;
        _localizedStringsService = localizedStringsService;
    }

    public async Task<CreateSinger.Response> CreateAsync(
        CreateSinger.Command command,
        CancellationToken cancellationToken = default
    )
    {
        var profilePicture = await _filesService.CreateAsync(command.ProfilePictureFile, cancellationToken);

        var singer = new Singer
        {
            Names = command.Names
                .Select(x => _localizedStringsService.Add(x.Text, x.Language)).ToList(),
            Nicknames = command.Nicknames
                .Select(x => _localizedStringsService.Add(x.Text, x.Language)).ToList(),
            Descriptions = command.Descriptions
                .Select(x => _localizedStringsService.Add(x.Text, x.Language)).ToList(),
            Activities = command.Activities
                .Select(x => _localizedStringsService.Add(x.Text, x.Language)).ToList(),
            Birthday = command.Birthday ?? DateTime.MinValue,
            BloodType = command.BloodType,
            Height = command.Height,
            Nationality = command.Nationality,
            ProfilePictureId = profilePicture.Id,
            ProfilePicture = profilePicture
        };

        await _context.Singers.AddAsync(singer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateSinger.Response
        {
            Id = singer.Id
        };
    }

    public async Task<bool> DeleteAsync(DeleteSinger.Command command, CancellationToken cancellationToken = default)
    {
        var singer = await _context.Singers
            .Include(x => x.Names)
            .Include(x => x.Nicknames)
            .Include(x => x.Descriptions)
            .Include(x => x.Activities)
            .Include(x => x.ProfilePicture)
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (singer == null)
        {
            return false;
        }

        _localizedStringsService.RemoveRange(singer.Names);
        _localizedStringsService.RemoveRange(singer.Nicknames);
        _localizedStringsService.RemoveRange(singer.Descriptions);
        _localizedStringsService.RemoveRange(singer.Activities);

        await _filesService.DeleteAsync(singer.ProfilePicture, cancellationToken);
        _context.Singers.Remove(singer);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<EditSinger.Response?> EditAsync(
        EditSinger.Command command,
        CancellationToken cancellationToken = default
    )
    {
        var singer = await _context
            .Singers
            .Include(x => x.Names)
            .Include(x => x.Nicknames)
            .Include(x => x.Descriptions)
            .Include(x => x.Activities)
            .Include(x => x.ProfilePicture)
            .FirstOrDefaultAsync(x => x.Id == command.SingerId, cancellationToken);

        if (singer == null)
        {
            return null;
        }

        if (command.Names.Any())
        {
            _localizedStringsService.RemoveRange(singer.Names);
            singer.Names = command.Names
                .Select(x => _localizedStringsService.Add(x.Text, x.Language))
                .ToList();
        }

        if (command.Nicknames.Any())
        {
            _localizedStringsService.RemoveRange(singer.Nicknames);
            singer.Nicknames = command.Nicknames
                .Select(x => _localizedStringsService.Add(x.Text, x.Language))
                .ToList();
        }

        if (command.Descriptions.Any())
        {
            _localizedStringsService.RemoveRange(singer.Descriptions);
            singer.Descriptions = command.Descriptions
                .Select(x => _localizedStringsService.Add(x.Text, x.Language))
                .ToList();
        }

        if (command.Activities.Any())
        {
            _localizedStringsService.RemoveRange(singer.Activities);
            singer.Activities = command.Activities
                .Select(x => _localizedStringsService.Add(x.Text, x.Language))
                .ToList();
        }

        if (command.Birthday != null && command.Birthday > DateTime.MinValue)
        {
            singer.Birthday = command.Birthday.Value;
        }

        if (!string.IsNullOrWhiteSpace(command.BloodType))
        {
            singer.BloodType = command.BloodType;
        }

        if (command.Height > 0)
        {
            singer.Height = command.Height;
        }

        if (!string.IsNullOrWhiteSpace(command.Nationality))
        {
            singer.Nationality = command.Nationality;
        }

        if (command.ProfilePictureFile != null)
        {
            await _filesService.DeleteAsync(singer.ProfilePicture, cancellationToken);
            singer.ProfilePicture = await _filesService.CreateAsync(command.ProfilePictureFile, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new EditSinger.Response
        {
            Id = singer.Id
        };
    }

    public Singer? GetById(Guid id)
    {
        return _context
            .Singers
            .FirstOrDefault(x => x.Id == id);
    }

    public Task<GetSinger.Response?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context
            .Singers
            .ProjectTo<GetSinger.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<GetSingers.Response>> GetAsync(
        GetSingers.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Singers
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<GetSingers.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<GetSingersForAdmin.Response>> GetForAdminAsync(
        GetSingersForAdmin.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Singers
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<GetSingersForAdmin.Response>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Singer>> SearchAsync(
        SearchSingers.Request request,
        CancellationToken cancellationToken = default
    )
    {
        return await _context.Singers
            .Include(x => x.Names)
            .Where(s => s.Names.Any(
                x => x.Text.ToLower().Contains(request.Input.ToLower()))
            )
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _context.Singers.CountAsync(cancellationToken);
    }
}