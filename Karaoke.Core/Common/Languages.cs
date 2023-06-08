using Karaoke.Core.Entities;

namespace Karaoke.Core.Common;

/// <summary>
///     Provides a set of predefined languages.
/// </summary>
public static class Languages
{
    public static readonly string English = "English";

    public static readonly Culture French = new("fr-FR");

    public static readonly Culture Japanese = new("ja-JP");

    public static readonly Culture Chinese = new("zh-CN");
}