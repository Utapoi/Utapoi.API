using Utapoi.Application.Common.Requests;
using Utapoi.Application.Lyrics;
using Utapoi.Application.Persistence;
using Utapoi.Core.Entities;

namespace Utapoi.Infrastructure.Lyrics;

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