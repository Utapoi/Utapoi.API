using FluentResults;
using MediatR;
using Utapoi.Application.DTO;
using Utapoi.Application.Users.Interfaces;

namespace Utapoi.Application.Users.Requests.GetCurrentUser;

public static class GetCurrentUser
{
    public record Request : IRequest<Result<Response>>;

    public sealed class Response
    {
        public UserDTO? User { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Request, Result<Response>>
    {
        public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Result.Fail("Not implemented yet.");
        }
    }
}