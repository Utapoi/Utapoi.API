﻿using Karaoke.Application.Albums.Commands.CreateAlbum;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Albums;

public interface IAlbumsService
{
    Album? GetById(Guid id);

    Task<Album?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Album> CreateAsync(CreateAlbum.Command command, CancellationToken cancellationToken);

    Task<IEnumerable<Album>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Album>> SearchAsync(string input, CancellationToken cancellationToken);
}