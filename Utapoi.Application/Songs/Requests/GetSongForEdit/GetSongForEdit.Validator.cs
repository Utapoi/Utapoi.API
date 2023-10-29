using FluentValidation;
using JetBrains.Annotations;

namespace Utapoi.Application.Songs.Requests.GetSongForEdit;

public static partial class GetSongForEdit
{
    [UsedImplicitly]
    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.SongId)
                .NotEqual(Guid.Empty)
                .NotNull();
        }
    }
}