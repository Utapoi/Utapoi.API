using Utapoi.Application.Common.Requests;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Karaoke;

public interface IKaraokeService
{
    Task<KaraokeInfo> CreateAsync(
        LocalizedFileRequest request,
        Song song,
        CancellationToken cancellationToken = default
    );
}