using FluentResults;
using Karaoke.Application.DTO;
using Karaoke.Application.Users.Interfaces;
using MediatR;

namespace Karaoke.Application.Users.Requests.GetCurrentUser;

public static class GetCurrentUser
{
    public record Request : IRequest<Result<Response>>;

    public sealed class Response
    {
        public UserDTO? User { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly IUsersService _usersService;

        public Handler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            return _usersService.GetCurrentUserAsync(cancellationToken);
        }
    }
}