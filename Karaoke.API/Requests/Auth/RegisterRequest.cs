using System.ComponentModel.DataAnnotations;

namespace Karaoke.API.Requests.Auth;

/// <summary>
///     Represents a request to register a new user.
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    ///     The username of the user.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///     The email of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     The password of the user.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}