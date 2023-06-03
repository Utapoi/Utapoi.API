namespace Karaoke.Application.Users.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}