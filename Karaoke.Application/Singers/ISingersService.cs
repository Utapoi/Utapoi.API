using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Application.Singers.Requests.GetSinger;
using Karaoke.Application.Singers.Requests.GetSingers;
using Karaoke.Application.Singers.Requests.GetSingersForAdmin;
using Karaoke.Application.Singers.Requests.SearchSingers;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Singers;

public interface ISingersService
{
    Task<Singer> CreateAsync(CreateSinger.Command command, CancellationToken cancellationToken);

    Singer? GetById(Guid id);

    Task<GetSinger.Response?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<GetSingers.Response>> GetAsync(
        GetSingers.Request request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<GetSingersForAdmin.Response>> GetForAdminAsync(
        GetSingersForAdmin.Request request,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<Singer>> SearchAsync(
        SearchSingers.Request request,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(CancellationToken cancellationToken = default);
}