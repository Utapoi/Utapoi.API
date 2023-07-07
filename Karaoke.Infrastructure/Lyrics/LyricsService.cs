using Karaoke.Application.Common.Requests;
using Karaoke.Application.Lyrics;
using Karaoke.Application.Persistence;
using Karaoke.Core.Entities;

namespace Karaoke.Infrastructure.Lyrics;

public sealed class LyricsService : ILyricsService
{
    private readonly IKaraokeDbContext _context;

    public LyricsService(IKaraokeDbContext context)
    {
        _context = context;
    }

    public Core.Entities.Lyrics Create(LocalizedStringRequest request, Song song)
    {
        var lyrics = _context.Lyrics.Add(new Core.Entities.Lyrics
        {
            Language = request.Language,
            Phrases = request.Text.Split(Environment.NewLine).ToList(),
            SongId = song.Id,
            Song = song,
        });

        _context.SaveChanges();

        return lyrics.Entity;
    }
}