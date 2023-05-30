using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Common;

/// <summary>
///     Provides a set of predefined languages.
/// </summary>
public static class Languages
{
    public static readonly Culture English = new("en-US");

    public static readonly Culture French = new("fr-FR");

    public static readonly Culture Japanese = new("ja-JP");

    public static readonly Culture Chinese = new("zh-CN");
}