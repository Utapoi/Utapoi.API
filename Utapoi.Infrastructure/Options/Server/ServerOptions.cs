using System.ComponentModel.DataAnnotations;

namespace Utapoi.Infrastructure.Options.Server;

public class ServerOptions : IValidatableObject
{
    public string BaseUrl { get; set; } = string.Empty;

    public string FileStoragePath { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            yield return new ValidationResult("No BaseUrl defined in ServerOptions configuration.",
                new[] { nameof(BaseUrl) });
        }

        if (string.IsNullOrWhiteSpace(FileStoragePath))
        {
            yield return new ValidationResult("No FileStoragePath defined in ServerOptions configuration.",
                new[] { nameof(FileStoragePath) });
        }
    }
}