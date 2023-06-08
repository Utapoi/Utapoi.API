using Karaoke.Core.Common;
using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

/// <summary>
///     Represents a song.
/// </summary>
public sealed class Song : AuditableEntity
{
    /// <summary>
    ///     Gets the titles of the song.
    /// </summary>
    public ICollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

    /// <summary>
    ///     Gets or sets the duration of the song.
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.Zero;

    /// <summary>
    ///     Gets or sets the release date of the song.
    /// </summary>
    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     Gets or sets the original language of the song.
    /// </summary>
    public Culture OriginalLanguage { get; set; } = Languages.Japanese;

    public Guid? ThumbnailId { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="Singer" /> thumbnail.
    /// </summary>
    public NamedFile? Thumbnail { get; set; }

    public Guid? VocalId { get; set; }

    /// <summary>
    ///     Gets or sets the vocal file path of the song.
    /// </summary>
    public NamedFile? Vocal { get; set; }

    public Guid? InstrumentalId { get; set; }

    /// <summary>
    ///     Gets or sets the instrumental file path of the song.
    /// </summary>
    public NamedFile? Instrumental { get; set; }

    public Guid? PreviewId { get; set; }

    public NamedFile? Preview { get; set; }

    /// <summary>
    ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="Album" />s which the song belongs to.
    /// </summary>
    public ICollection<Album> Albums { get; set; } = new List<Album>();

    /// <summary>
    ///     Gets the <see cref="Entities.Lyrics" /> of the song.
    /// </summary>
    public ICollection<Lyrics> Lyrics { get; set; } = new List<Lyrics>();

    /// <summary>
    ///     Gets the <see cref="KaraokeInfo" /> of the song.
    /// </summary>
    public ICollection<KaraokeInfo> Karaoke { get; set; } = new List<KaraokeInfo>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Singer" />s who sang the song.
    /// </summary>
    public ICollection<Singer> Singers { get; set; } = new List<Singer>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Composer" />s who composed the song.
    /// </summary>
    public ICollection<Composer> Composers { get; set; } = new List<Composer>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="SongWriter" />s who wrote the song.
    /// </summary>
    public ICollection<SongWriter> SongWriters { get; set; } = new List<SongWriter>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Work" />s the song appears in.
    /// </summary>
    public ICollection<Work> Sources { get; set; } = new List<Work>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Collection" />s the song belongs to.
    /// </summary>
    public ICollection<Collection> Collections { get; set; } = new List<Collection>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Tag" />s the song is tagged with.
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();


    public string GetTitle(string language)
    {
        return Titles.FirstOrDefault(t => t.Language == language)?.Text ?? string.Empty;
    }
}