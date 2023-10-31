namespace Utapoi.Application.Common.Requests;

public class LocalizedStringRequest
{
    public string Id { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;
}