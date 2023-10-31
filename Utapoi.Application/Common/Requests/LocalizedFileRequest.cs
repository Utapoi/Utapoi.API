namespace Utapoi.Application.Common.Requests;

/// <summary>
///     Represents a localized file.
/// </summary>
public sealed class LocalizedFileRequest
{
    /// <summary>
    ///     Gets or sets the file.
    /// </summary>
    public byte[] File { get; set; } = Array.Empty<byte>();

    public string FileName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the type of the file.
    /// </summary>
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the language.
    /// </summary>
    public string Language { get; set; } = string.Empty;
}