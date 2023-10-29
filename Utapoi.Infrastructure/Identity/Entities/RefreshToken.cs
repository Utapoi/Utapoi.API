using Utapoi.Core.Entities.Common;

namespace Utapoi.Infrastructure.Identity.Entities;

public sealed class RefreshToken : AuditableEntity
{
    public Guid TokenId { get; set; }

    public Token AccessToken { get; set; } = null!;

    public string Token { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public int UsageCount { get; set; } = 0;

    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow;

    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public int ExpiresIn => (int)ExpiresAt.Subtract(DateTime.UtcNow).TotalSeconds;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsValid => UsageCount == 0 && !IsExpired;
}