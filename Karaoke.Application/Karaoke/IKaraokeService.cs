using Karaoke.Application.Common.Requests;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Karaoke;

public interface IKaraokeService
{
    Task<KaraokeInfo> CreateAsync(
        LocalizedFileRequest request,
        Song song,
        CancellationToken cancellationToken = default
    );
}