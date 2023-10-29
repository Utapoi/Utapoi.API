﻿using Utapoi.Core.Entities.Common;

namespace Utapoi.Core.Entities;

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
    public ICollection<string> Languages { get; } = new List<string>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="KaraokeInfo" /> created by the user.
    /// </summary>
    public ICollection<KaraokeInfo> Karaoke { get; } = new List<KaraokeInfo>();
}