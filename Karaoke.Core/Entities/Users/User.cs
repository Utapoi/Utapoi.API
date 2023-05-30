using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities.Users;

/// <summary>
///     Represents a user.
/// </summary>
public sealed class User : AuditableEntity
{
    /// <summary>
    ///     Gets or sets the username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the profile picture.
    /// </summary>
    public string ProfilePicture { get; set; } = string.Empty;

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Culture" /> of the languages the user speaks.
    /// </summary>
    /// <remarks>
    ///     The default language is <see cref="Core.Common.Languages.English" />.
    ///     They are sorted by preference.
    /// </remarks>
    public ICollection<Culture> Languages { get; } = new List<Culture>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Songs.Karaoke" /> created by the user.
    /// </summary>
    public ICollection<Songs.Karaoke> Karaoke { get; } = new List<Songs.Karaoke>();
}