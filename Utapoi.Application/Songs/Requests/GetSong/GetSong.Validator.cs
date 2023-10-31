using FluentValidation;
using JetBrains.Annotations;

namespace Utapoi.Application.Songs.Requests.GetSong;

public static partial class GetSong
{
    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}