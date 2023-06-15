namespace Karaoke.Application.Identity.GoogleAuth;

/// <summary>
///     Properties for the Google authentication handler.
/// </summary>
/// <remarks>
///     This is just a simplified version of the <c>Microsoft.AspNetCore.Authentication.AuthenticationProperties</c> class
///     to avoid a dependency on the ASP.NET Core package in the application layer.
/// </remarks>
public sealed class GoogleAuthProperties
{
    /// <summary>
    ///     State values about the authentication session.
    /// </summary>
    public IDictionary<string, string?> Items { get; set; } = new Dictionary<string, string?>();

    /// <summary>
    ///     Collection of parameters that are passed to the authentication handler. These are not intended for
    ///     serialization or persistence, only for flowing data between call sites.
    /// </summary>
    public IDictionary<string, object?> Parameters { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    ///     Allow refreshing the authentication session.
    /// </summary>
    public bool AllowRefresh { get; set; }
}