using System.Linq.Expressions;
using Utapoi.Application.Songs.Commands.CreateSong;
using Utapoi.Application.Songs.Requests.GetSong;
using Utapoi.Application.Songs.Requests.GetSongForEdit;
using Utapoi.Application.Songs.Requests.GetSongsForAdmin;
using Utapoi.Application.Songs.Requests.GetSongsForSinger;
using Utapoi.Core.Entities;
using Utapoi.Core.Exceptions;

namespace Utapoi.Application.Songs;

/// <summary>
///     The <see cref="ISongsService" /> interface.
/// </summary>
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
    ///     The <see cref="GetSongsForAdmin.Request" /> containing the pagination request.
    /// </param>
    /// <param name="cancellationToken">
    ///     The <see cref="CancellationToken" /> used to cancel the operation.
    /// </param>
    /// <returns>
    ///     An <see cref="IReadOnlyCollection{T}" /> of <see cref="Song" />s.
    /// </returns>
    Task<IReadOnlyCollection<GetSongsForAdmin.Response>> GetForAdminAsync(
        GetSongsForAdmin.Request request,
        CancellationToken cancellationToken = default
    );

    Task<GetSongForEdit.Response> GetForEditAsync(
        GetSongForEdit.Request request,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Gets a <see cref="Song" /> by Id.
    /// </summary>
    /// <param name="id">The id of the song.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Song" />.
    /// </returns>
    /// <exception cref="EntityNotFoundException{T}">
    ///     Thrown when the <see cref="Song" /> is not found.
    /// </exception>
    Task<GetSong.Response> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Gets all <see cref="Song" />s for a <see cref="Singer" />.
    /// </summary>
    /// <param name="request">The <see cref="GetSongsForSinger.Request" /> containing the pagination request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     An <see cref="IReadOnlyCollection{T}" /> of <see cref="Song" />s.
    /// </returns>
    Task<IReadOnlyCollection<GetSongsForSinger.Response>> GetForSingerAsync(
        GetSongsForSinger.Request request,
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

    Task<int> CountAsync(Expression<Func<Song, bool>> predicate, CancellationToken cancellationToken = default);
}