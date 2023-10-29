using System.ComponentModel.DataAnnotations;

namespace Utapoi.Infrastructure.Options.Google;

public class GoogleAuthOptions
{
    [Required]
    public string ClientId { get; set; } = string.Empty;

    [Required]
    public string ClientSecret { get; set; } = string.Empty;

    [Required]
    public string RedirectUrl { get; set; } = string.Empty;

    [Required]
    public string WebClientUrl { get; set; } = string.Empty;

    public IEnumerable<string> Scopes { get; set; } = new List<string>();
}