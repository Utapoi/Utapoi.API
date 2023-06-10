using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Singers;

public interface ISingersService
{
    Singer? GetById(Guid id);

    Task<Singer> CreateAsync(CreateSinger.Command command, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Singer>> GetAsync(
        GetSingers.Request request,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}