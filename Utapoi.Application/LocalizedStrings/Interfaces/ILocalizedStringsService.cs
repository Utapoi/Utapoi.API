using Utapoi.Core.Entities;

namespace Utapoi.Application.LocalizedStrings.Interfaces;

public interface ILocalizedStringsService
{
    LocalizedString Add(string text, string language);

    Task<LocalizedString> AddAsync(string text, string language, CancellationToken cancellationToken = default);

    public void RemoveRange(IEnumerable<LocalizedString> localizedStrings);
}