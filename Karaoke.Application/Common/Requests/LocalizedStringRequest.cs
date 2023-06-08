namespace Karaoke.Application.Common.Requests;

public class LocalizedStringRequest
{
    public string Text { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;
}