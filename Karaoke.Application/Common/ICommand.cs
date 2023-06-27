using MediatR;

namespace Karaoke.Application.Common;

public interface ICommand<T> : IRequest<T>
{
}
