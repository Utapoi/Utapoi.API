using MediatR;

namespace Utapoi.Application.Common;

public interface ICommand<T> : IRequest<T>
{
}
