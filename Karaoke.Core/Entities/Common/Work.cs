namespace Karaoke.Core.Entities.Common;

/// <summary>
///     Represents a work.
/// </summary>
public sealed class Work : AuditableEntity
{
    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the names of the work.
    /// </summary>
    public ICollection<LocalizedString> Names { get; set; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="LocalizedString" />s representing the descriptions of the work.
    /// </summary>
    public ICollection<LocalizedString> Descriptions { get; set; } = new List<LocalizedString>();
}