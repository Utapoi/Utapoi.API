using AutoMapper;
using FluentResults;
using Karaoke.Application.DTO;
using Karaoke.Application.Users.Interfaces;
using Karaoke.Application.Users.Requests.GetCurrentUser;
using Karaoke.Core.Entities;
using Karaoke.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Karaoke.Infrastructure.Users;

public class UsersService : IUsersService
{
    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public UsersService(
        ICurrentUserService currentUserService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<GetCurrentUser.Response>> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    )
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
            ProfilePicture = user.ProfilePicture ?? string.Empty
        });

        u.Roles = await _userManager.GetRolesAsync(user);

        return Result.Ok(new GetCurrentUser.Response
        {
            User = u
        });
    }

    public async Task<Result> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return Result.Fail("User not found.");
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            return Result.Fail("Role not found.");
        }

        if (!await _userManager.IsInRoleAsync(user, role))
        {
            return Result.Fail("User is not in role.");
        }

        return Result.Ok();
    }
}