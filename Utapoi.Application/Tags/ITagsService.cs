using Utapoi.Core.Entities;

namespace Utapoi.Application.Tags;

public interface ITagsService
{
    Tag GetOrCreateByName(string name);

    Tag? GetById(Guid id);
}