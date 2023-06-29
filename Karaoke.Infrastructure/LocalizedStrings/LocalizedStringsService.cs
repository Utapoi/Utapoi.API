using Karaoke.Application.LocalizedStrings.Interfaces;
using Karaoke.Application.Persistence;
using Karaoke.Core.Entities;

namespace Karaoke.Infrastructure.LocalizedStrings;

public sealed class LocalizedStringsService : ILocalizedStringsService
{
    private readonly IKaraokeDbContext _context;

    public LocalizedStringsService(IKaraokeDbContext context)
    {
        _context = context;
    }

    public LocalizedString Add(string text, string language)
    {
        return _context.LocalizedStrings.Add(new LocalizedString
        {
            Text = text,
            Language = language
        }).Entity;
    }

    public async Task<LocalizedString> AddAsync(string text, string language, CancellationToken cancellationToken = default)
    {
        return (await _context.LocalizedStrings.AddAsync(new LocalizedString
        {
            Text = text,
            Language = language
        }, cancellationToken)).Entity;
    }

    public void RemoveRange(IEnumerable<LocalizedString> localizedStrings)
    {
        _context.LocalizedStrings.RemoveRange(localizedStrings);
    }
}