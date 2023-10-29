using System.ComponentModel.DataAnnotations;

namespace Utapoi.Infrastructure.Options.Admin;

public sealed class AdminOptions : IValidatableObject
{
    public IReadOnlyCollection<string> AllowedEmails { get; set; } = Array.Empty<string>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AllowedEmails.Count == 0)
        {
            yield return new ValidationResult("No AllowedEmails defined in AdminOptions configuration.",
                new[] { nameof(AllowedEmails) });
        }
    }
}