using Karaoke.Core.Common;
using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

/// <summary>
///     Represents a karaoke.
/// </summary>
public sealed class KaraokeInfo : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Language { get; set; } = Languages.English;

    public int SingersCount { get; set; }

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="User" /> who created the karaoke.
    /// </summary>
    public ICollection<User> Creators { get; } = new List<User>();

    /// <summary>
    ///     Gets or sets the id of the associated <see cref="Song" />.
    /// </summary>
    public Guid SongId { get; set; }

    /// <summary>
    ///     Gets or sets the associated <see cref="Song" />.
    /// </summary>
    public Song Song { get; set; } = null!;

    public Guid FileId { get; set; }

    public NamedFile File { get; set; } = null!;
}