using FluentValidation;

namespace Karaoke.Application.Singers.Commands.CreateSinger
{
    public static partial class CreateSinger
    {
        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(x => x.Names)
                    .Must(x => !string.IsNullOrWhiteSpace(x.Text) && !string.IsNullOrWhiteSpace(x.Language))
                    .WithMessage("You must at least provide one valid Name for the singer.");
            }
        }
    }
}