namespace Karaoke.Application.Common.Extensions;

public static class StringExtensions
{
    public static string ReplaceInvalidChars(this string filename, string replacement = "_")
    {
        return string.Join(replacement, filename.Split(Path.GetInvalidFileNameChars()));
    }

    public static string GetExtensionFromContentType(this string contentType)
    {
        return contentType switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            "image/tiff" => ".tiff",
            "image/svg+xml" => ".svg",
            "image/webp" => ".webp",
            "subtitle/ass" => ".ass",
            "audio/ogg" => ".ogg",
            _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null)
        };
    }
}