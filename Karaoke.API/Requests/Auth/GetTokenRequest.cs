using System.ComponentModel.DataAnnotations;

namespace Karaoke.API.Requests.Auth;

/// <summary>
///     Represents a request to get a token.
/// </summary>
public class GetTokenRequest
{
    /// <summary>
    ///     The username of the user.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///     The password of the user.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}