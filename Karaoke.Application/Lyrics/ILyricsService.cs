using Karaoke.Application.Common.Requests;
using Karaoke.Core.Entities;

namespace Karaoke.Application.Lyrics;

public interface ILyricsService
{
    Core.Entities.Lyrics Create(LocalizedStringRequest request, Song song);
}