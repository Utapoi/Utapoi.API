using AutoMapper;
using FluentResults;
using Karaoke.Application.DTO;
using Karaoke.Application.Users.Interfaces;
using Karaoke.Application.Users.Requests.GetCurrentUser;
using Karaoke.Core.Entities;
using Karaoke.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Users.Services;

public class UsersService : IUsersService
{
    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersService(ICurrentUserService currentUserService, IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Result<GetCurrentUser.Response>> GetCurrentUserAsync(
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(_currentUserService.UserId ?? string.Empty);

        if (user == null)
        {
            return Result.Fail("User not found.");
        }

        var u = _mapper.Map<UserDTO>(new User
        {
            Id = Guid.Parse(user.Id),
            Username = user.UserName ?? string.Empty,
            ProfilePicture = user.ProfilePicture
        });

        u.Roles = await _userManager.GetRolesAsync(user);

        return Result.Ok(new GetCurrentUser.Response
        {
            User = u
        });
    }
}