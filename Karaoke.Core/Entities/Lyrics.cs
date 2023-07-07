using System.ComponentModel.DataAnnotations;
using Karaoke.Core.Common;
using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

/// <summary>
///     Represents the lyrics of a song in a specific language.
/// </summary>
public sealed class Lyrics : AuditableEntity
{
    /// <summary>
    ///     Gets the phrases of the lyrics.
    /// </summary>
    public List<string> Phrases { get; set; } = new List<string>();

    /// <summary>
    ///     Gets or sets the language.
    /// </summary>
    /// <remarks>
    ///     The default language is <see cref="Languages.English" />.
    /// </remarks>
    public string Language { get; set; } = Languages.English;

    /// <summary>
    ///     Gets or sets the <see cref="Entities.Song" /> identifier linked to this <see cref="Lyrics" />.
    /// </summary>
    [Required]
    public Guid SongId { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="Entities.Song" /> linked to this <see cref="Lyrics" />.
    /// </summary>
    [Required]
    public Song Song { get; set; } = null!;
}