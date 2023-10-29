using System.ComponentModel.DataAnnotations;

namespace Utapoi.API.Requests.Auth;

/// <summary>
///     Represents a request to refresh a token.
/// </summary>
public sealed class RefreshTokenRequest
{
    /// <summary>
    ///     The token to refresh.
    /// </summary>
    [Required]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    ///     The refresh token.
    /// </summary>
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}