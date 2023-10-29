namespace Utapoi.Application.Identity.GoogleAuth;

/// <summary>
///     Properties for the Google authentication handler.
/// </summary>
/// <remarks>
///     This is just a simplified version of the <c>Microsoft.AspNetCore.Authentication.AuthenticationProperties</c> class
///     to avoid a dependency on the ASP.NET Core package in the application layer.
/// </remarks>
public sealed class GoogleAuthProperties
{
    public string Provider { get; init; } = string.Empty;

    public string RedirectUrl { get; init; } = string.Empty;
    
    /// <summary>
    ///     Allow refreshing the authentication session.
    /// </summary>
    public bool AllowRefresh { get; init; }
}