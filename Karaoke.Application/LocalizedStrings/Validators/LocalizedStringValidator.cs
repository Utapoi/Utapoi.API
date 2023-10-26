using FluentValidation;
using Karaoke.Application.Common.Requests;

namespace Karaoke.Application.LocalizedStrings.Validators;

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