using Karaoke.Application.Songs.Commands.CreateSong;
using Karaoke.Application.Songs.Requests.GetSongs;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Songs;

public interface ISongsService
{
    /// <summary>
    ///     Creates a new <see cref="Song" />.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateSong.Command" /> containing the information needed to create a new song.
    /// </param>
    /// <param name="cancellationToken">
    ///     The <see cref="CancellationToken" /> used to cancel the operation.
    /// </param>
    /// <returns>
    ///     The ID of the newly created <see cref="Song" />.
    /// </returns>
    Task<string> CreateAsync(CreateSong.Command command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all <see cref="Song" />s.
    /// </summary>
    /// <param name="request">
    ///     The <see cref="GetSongs.Request" /> containing the pagination request.
    /// </param>
    /// <param name="cancellationToken">
    ///     The <see cref="CancellationToken" /> used to cancel the operation.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyCollection{T}" /> of <see cref="Song" />s.
    /// </returns>
    Task<IReadOnlyCollection<Song>> GetSongsAsync(
        GetSongs.Request request,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Gets the number of <see cref="Song" />s.
    /// </summary>
    /// <param name="cancellationToken">
    ///     The <see cref="CancellationToken" /> used to cancel the operation.
    /// </param>
    /// <returns>
    ///     The number of <see cref="Song" />s.
    /// </returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}