namespace Utapoi.Application.Singers.Commands.EditSinger;

public static partial class EditSinger
{
    public sealed class Response
    {
        public Guid Id { get; init; } = Guid.Empty;
    }
}