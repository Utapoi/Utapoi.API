using Karaoke.Application.Common.Models;

namespace Karaoke.Application.Auth.Requests.LoginUser;

public sealed class LoginUserResponse
{
    public Result Result { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;
}