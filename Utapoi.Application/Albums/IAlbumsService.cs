﻿using Utapoi.Application.Albums.Commands.CreateAlbum;
using Utapoi.Application.Albums.Requests.GetAlbumsForAdmin;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Albums;

public interface IAlbumsService
{
    Album? GetById(Guid id);

    Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Album> CreateAsync(CreateAlbum.Command command, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<GetAlbumsForAdmin.Response>> GetForAdminAsync(
        GetAlbumsForAdmin.Request request,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Album>> SearchAsync(string input, CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}