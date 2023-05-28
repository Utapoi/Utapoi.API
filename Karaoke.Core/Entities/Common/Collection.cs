using Karaoke.Core.Entities.Songs;

namespace Karaoke.Core.Entities.Common;

public sealed class Collection : AuditableEntity
{
    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the names of the
    ///     collection.
    /// </summary>
    public ICollection<LocalizedString> Names { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" /> representing the songs in the collection.
    /// </summary>
    public ICollection<Song> Songs { get; } = new List<Song>();
}