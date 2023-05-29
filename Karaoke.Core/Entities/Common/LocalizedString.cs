using System.Globalization;
using Karaoke.Core.Common;

namespace Karaoke.Core.Entities.Common;

/// <summary>
///     Represents a localized string.
/// </summary>
public sealed class LocalizedString : Entity
{
    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the language.
    /// </summary>
    /// <remarks>
    ///     The default language is <see cref="Languages.English" />.
    /// </remarks>
    public CultureInfo Language { get; set; } = Languages.English;
}