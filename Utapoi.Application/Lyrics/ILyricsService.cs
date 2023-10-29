using Utapoi.Application.Common.Requests;
using Utapoi.Core.Entities;

namespace Utapoi.Application.Lyrics;

public interface ILyricsService
{
    Core.Entities.Lyrics Create(LocalizedStringRequest request, Song song);
}