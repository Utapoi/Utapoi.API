using FluentResults;
using Karaoke.Application.Common;

namespace Karaoke.Application.Singers.Commands.DeleteSinger;

public static partial class DeleteSinger
{
    public sealed class Command : ICommand<Result<Response>>
    {
        public Command(Guid id)
        {
            Id = id;
        }

        public Command()
        {
        }

        public Guid Id { get; init; }
    }
}