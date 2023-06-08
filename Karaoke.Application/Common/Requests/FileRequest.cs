namespace Karaoke.Application.Common.Requests;

/// <summary>
///     Represents a file.
/// </summary>
public sealed class FileRequest
{
    /// <summary>
    ///     Gets or sets the file.
    /// </summary>
    public byte[] File { get; set; } = Array.Empty<byte>();

    /// <summary>
    ///     Gets or sets the type of the file.
    /// </summary>
    public string FileType { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
}