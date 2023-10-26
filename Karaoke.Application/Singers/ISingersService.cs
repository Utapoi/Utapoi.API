﻿using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Commands.DeleteSinger;
using Karaoke.Application.Singers.Commands.EditSinger;
using Karaoke.Application.Singers.Requests.GetSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Karaoke.Application.Singers.Requests.SearchSingers;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Singers;

public interface ISingersService
{
    /// <summary>
    /// Creates a new singer.
    /// </summary>
    /// <param name="command">A command to create a singer.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A <see cref="CreateSinger.Response"/> containing the created singer.
    /// </returns>
    Task<CreateSinger.Response> CreateAsync(CreateSinger.Command command, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(DeleteSinger.Command command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edits an existing singer.
    /// </summary>
    /// <param name="command">A command to edit a singer.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// The <see cref="EditSinger.Response"/> containing the edited singer.
    /// </returns>
    Task<EditSinger.Response?> EditAsync(EditSinger.Command command, CancellationToken cancellationToken = default);

    Singer? GetById(Guid id);

    Task<Singer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Singer>> GetAsync(
        GetSingers.Request request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<Singer>> GetForAdminAsync(
        GetSingersForAdmin.Request request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<Singer>> SearchAsync(
        SearchSingers.Request request,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}