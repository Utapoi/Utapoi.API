using Utapoi.Application.LocalizedStrings.Interfaces;
using Utapoi.Application.Persistence;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.LocalizedStrings;

public sealed class LocalizedStringsService : ILocalizedStringsService
{
    private readonly IUtapoiDbContext _context;

    public LocalizedStringsService(IUtapoiDbContext context)
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