using Karaoke.Core.Entities.Common;
using Karaoke.Core.Entities.Songs;

namespace Karaoke.Core.Entities.Artists;

/// <summary>
///     Represents a singer.
/// </summary>
public sealed class Singer : AuditableEntity
{
    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the names of the singer.
    /// </summary>
    public ICollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the nicknames of the singer.
    /// </summary>
    public ICollection<LocalizedString> Nicknames { get; set; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}