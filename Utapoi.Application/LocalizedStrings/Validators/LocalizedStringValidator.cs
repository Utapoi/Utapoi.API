using FluentValidation;
using Utapoi.Application.Common.Requests;

namespace Utapoi.Application.LocalizedStrings.Validators;

public sealed class LocalizedStringRequestValidator : AbstractValidator<LocalizedStringRequest>
{
    public LocalizedStringRequestValidator()
    {
        RuleFor(x => x.Language)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull();
    }
}