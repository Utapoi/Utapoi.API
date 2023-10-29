namespace Utapoi.Application.Auth.Responses;

public sealed class TokenResponse
{
    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime TokenExpiryTime { get; set; } = DateTime.UtcNow;

    public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;

    public TokenSource TokenSource { get; set; } = TokenSource.None;
}