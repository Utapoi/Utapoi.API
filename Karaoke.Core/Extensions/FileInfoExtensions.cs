using Karaoke.Core.Interfaces;

namespace Karaoke.Core.Extensions;

public static class FileInfoExtensions
{
    public static string GetStoragePath(this IFileInfo info)
    {
        return Path.Combine(info.Hash.Remove(2), info.Hash.Remove(4), info.Hash) + info.Extension;
    }

    public static string GetUrl(this IFileInfo info)
    {
        return $"/files/{info.GetStoragePath().Replace("\\", "/")}";
    }
}