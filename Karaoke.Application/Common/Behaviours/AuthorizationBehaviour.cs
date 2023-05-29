using System.Reflection;
using Karaoke.Application.Common.Attributes;
using Karaoke.Application.Common.Exceptions;
using Karaoke.Application.Interfaces;
using Karaoke.Application.Interfaces.Auth;
using MediatR;

namespace Karaoke.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService,
        IAuthService authService)
    {
        _currentUserService = currentUserService;
        _authService = authService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        // Must be authenticated user
        if (_currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }

        // Role-based authorization
        var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles))
            .ToList();

        if (authorizeAttributesWithRoles.Any())
        {
            var authorized = false;

            foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
            {
                foreach (var role in roles)
                {
                    var isInRole = await _authService.IsInRoleAsync(_currentUserService.UserId, role.Trim());
                    if (isInRole)
                    {
                        authorized = true;
                        break;
                    }
                }
            }

            // Must be a member of at least one role in roles
            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }

        // Policy-based authorization
        var authorizeAttributesWithPolicies =
            authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .ToList();

        if (!authorizeAttributesWithPolicies.Any())
        {
            return await next();
        }

        foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
        {
            var authorized = await _authService.AuthorizeAsync(_currentUserService.UserId, policy);

            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }

        return await next();
    }
}