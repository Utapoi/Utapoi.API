using Karaoke.Application.Singers.Commands.CreateSinger;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Singers;

public interface ISingersService
{
    Singer? GetById(Guid id);

    Task<Singer> CreateAsync(CreateSinger.Command command, CancellationToken cancellationToken);
}