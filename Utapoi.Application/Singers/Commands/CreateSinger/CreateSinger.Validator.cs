using FluentValidation;
using JetBrains.Annotations;

namespace Utapoi.Application.Singers.Commands.CreateSinger
{
    public static partial class CreateSinger
    {
        /// <summary>
        /// FluentValidation of the <see cref="CreateSinger.Command"/>.
        /// </summary>
        /// <remarks>
        /// The Validator is automatically registered in the DI container.
        /// </remarks>
        [UsedImplicitly]
        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(x => x.Names)
                    .Must(x => !string.IsNullOrWhiteSpace(x.Text) && !string.IsNullOrWhiteSpace(x.Language))
                    .WithMessage("You must at least provide one valid Name for the singer.");

                RuleForEach(x => x.Activities)
                    .Must(x => !string.IsNullOrWhiteSpace(x.Text) && !string.IsNullOrWhiteSpace(x.Language))
                    .WithMessage("You must at least provide one valid Activity for the singer.");
            }
        }
    }
}