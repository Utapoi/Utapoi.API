using Karaoke.Application.Common.Models;

namespace Karaoke.Application.Auth.Requests.RegisterUser;

public sealed class RegisterUserResponse
{
    public Result Result { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;
}