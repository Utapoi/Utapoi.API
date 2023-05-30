using System.ComponentModel.DataAnnotations;

namespace Karaoke.API.Requests.Auth;

public sealed class RegisterRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}