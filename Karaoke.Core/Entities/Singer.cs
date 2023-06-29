using Karaoke.Core.Common;
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

    /// <summary>
    ///   Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the descriptions of the singer.
    /// </summary>
    public ICollection<LocalizedString> Descriptions { get; set; } = new List<LocalizedString>();

    /// <summary>
    ///    Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the activities of the singer.
    /// </summary>
    public ICollection<LocalizedString> Activities { get; set; } = new List<LocalizedString>();

    public Guid ProfilePictureId { get; set; }

    /// <summary>
    ///    Gets or sets the profile picture.
    /// </summary>
    public NamedFile ProfilePicture { get; set; } = null!;

    /// <summary>
    ///    Gets an <see cref="ICollection{T}" /> of <see cref="Album" />s.
    /// </summary>
    public ICollection<Album> Albums { get; set; } = new List<Album>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; set; } = new List<Song>();

    /// <summary>
    ///     Gets or sets the date of birth of the singer.
    /// </summary>
    public DateTime Birthday { get; set; } = DateTime.MinValue;

    /// <summary>
    ///    Gets or sets the blood type of the singer.
    /// </summary>
    public string BloodType { get; set; } = string.Empty;

    /// <summary>
    ///   Gets or sets the height of the singer.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Gets or sets the nationality of the singer.
    /// </summary>
    /// <remarks>
    /// Same values as <see cref="Languages" />.
    /// </remarks>
    public string Nationality { get; set; } = string.Empty;
}