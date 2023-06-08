using Karaoke.Application.Songs.Commands.CreateSong;

namespace Karaoke.Application.Songs;

public interface ISongsService
{
    Task<string> CreateAsync(CreateSong.Command command, CancellationToken cancellationToken = default);
}