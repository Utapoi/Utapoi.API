using Karaoke.Core.Entities.Common;
using Karaoke.Core.Entities.Songs;

namespace Karaoke.Core.Entities.Artists;

/// <summary>
///     Represents a song writer.
/// </summary>
public sealed class SongWriter : AuditableEntity
{
    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the names of the
    ///     song writer.
    /// </summary>
    public ICollection<LocalizedString> Names { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the nicknames of the
    ///     song writer.
    /// </summary>
    public ICollection<LocalizedString> Nicknames { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; } = new List<Song>();
}