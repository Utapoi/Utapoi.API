﻿using Karaoke.Application.Common.Requests;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Files;

public interface IFilesService
{
    Task<NamedFile> CreateAsync(FileRequest request, CancellationToken cancellationToken = default);

    Task<NamedFile> CreateAsync(LocalizedFileRequest request, CancellationToken cancellationToken = default);
}