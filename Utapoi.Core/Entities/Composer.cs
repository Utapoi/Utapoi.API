using Utapoi.Core.Entities.Common;

namespace Utapoi.Core.Entities;

/// <summary>
///     Represents a composer.
/// </summary>
public sealed class Composer : AuditableEntity
{
    public ICollection<LocalizedString> Names { get; } = new List<LocalizedString>();

    public ICollection<LocalizedString> Nicknames { get; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Song" />s.
    /// </summary>
    public ICollection<Song> Songs { get; } = new List<Song>();
}