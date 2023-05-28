using Karaoke.Core.Entities.Songs;

namespace Karaoke.Core.Entities.Artists;

/// <summary>
///     Represents a composer.
/// </summary>
public sealed class Composer : AuditableEntity
{
    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the names of the composer.
    /// </summary>
    public ICollection<LocalizedString> Names { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the nicknames of the
    ///     composer.
    /// </summary>
    public ICollection<LocalizedString> Nicknames { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; } = new List<Song>();
}