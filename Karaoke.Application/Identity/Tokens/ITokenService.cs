using Karaoke.Application.Auth.Commands.GetToken;
using Karaoke.Application.Auth.Commands.RefreshToken;

namespace Karaoke.Application.Identity.Tokens;

public interface ITokenService
{
    Task<GetToken.Response> GetTokenAsync(GetToken.Command request);

    Task<RefreshToken.Response> RefreshTokenAsync(RefreshToken.Command request);
}