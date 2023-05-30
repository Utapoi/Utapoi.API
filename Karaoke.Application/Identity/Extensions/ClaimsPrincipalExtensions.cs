using System.Security.Claims;

namespace Karaoke.Application.Identity.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Email);
    }

    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
    {
        return principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
    }
}