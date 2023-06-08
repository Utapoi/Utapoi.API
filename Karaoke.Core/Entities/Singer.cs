using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

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

    public Guid ProfilePictureId { get; set; }

    public NamedFile ProfilePicture { get; set; } = null!;

    public ICollection<Album> Albums { get; set; } = new List<Album>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; set; } = new List<Song>();

    /// <summary>
    ///     Gets or sets the date of birth of the singer.
    /// </summary>
    public DateTime Birthday { get; set; } = DateTime.MinValue;
}