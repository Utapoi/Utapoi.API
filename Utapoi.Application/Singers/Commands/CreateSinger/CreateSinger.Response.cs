namespace Utapoi.Application.Singers.Commands.CreateSinger
{
    public static partial class CreateSinger
    {
        /// <summary>
        /// Represents a response of singer creation.
        /// </summary>
        public sealed class Response
        {
            public Guid Id { get; init; } = Guid.Empty;
        }
    }
}