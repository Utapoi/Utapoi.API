namespace Karaoke.Core.Exceptions;

public sealed class EntityNotFoundException<T> : Exception
{
    public EntityNotFoundException()
        : base($"Entity of type {typeof(T).Name} not found.")
    {
    }

    public EntityNotFoundException(Guid id)
        : base($"Entity of type {typeof(T).Name} with id {id} was not found.")
    {
    }
}