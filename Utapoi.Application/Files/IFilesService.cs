using Utapoi.Application.Common.Requests;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Files;

public interface IFilesService
{
    Task<NamedFile> CreateAsync(FileRequest request, CancellationToken cancellationToken = default);

    Task<NamedFile> CreateAsync(LocalizedFileRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(NamedFile? namedFile, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid namedFileId, CancellationToken cancellationToken = default);
}