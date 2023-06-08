using Karaoke.Core.Entities;

namespace Karaoke.Application.Tags;

public interface ITagsService
{
    Tag GetOrCreateByName(string name);

    Tag? GetById(Guid id);
}