using System.Globalization;

namespace Karaoke.Core.Common;

/// <summary>
///     Provides a set of predefined languages.
/// </summary>
public static class Languages
{
    public static readonly CultureInfo English = new("en-US");

    public static readonly CultureInfo French = new("fr-FR");

    public static readonly CultureInfo Japanese = new("ja-JP");

    public static readonly CultureInfo Chinese = new("zh-CN");
}