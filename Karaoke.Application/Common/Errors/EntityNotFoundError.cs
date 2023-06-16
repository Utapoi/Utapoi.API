using FluentResults;

namespace Karaoke.Application.Common.Errors;

public sealed class EntityNotFoundError : Error
{
    public EntityNotFoundError(string message, Guid id) : base(message)
    {
        WithMetadata("Id", id);
    }
}