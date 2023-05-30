using Karaoke.Core.Common;
using Karaoke.Core.Entities.Artists;
using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities.Songs;

/// <summary>
///     Represents a song.
/// </summary>
public sealed class Song : AuditableEntity
{
    /// <summary>
    ///     Gets the titles of the song.
    /// </summary>
    public ICollection<LocalizedString> Titles { get; } = new List<LocalizedString>();

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

    /// <summary>
    ///     Gets or sets the vocal file path of the song.
    /// </summary>
    public string VocalFilePath { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the vocal file name of the song.
    /// </summary>
    public string VocalFileName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the vocal file size of the song.
    /// </summary>
    public long VocalFileSize { get; set; }

    /// <summary>
    ///     Gets or sets the instrumental file path of the song.
    /// </summary>
    public string InstrumentalFilePath { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the instrumental file name of the song.
    /// </summary>
    public string InstrumentalFileName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the instrumental file size of the song.
    /// </summary>
    public long InstrumentalFileSize { get; set; }

    /// <summary>
    ///     Gets the <see cref="Songs.Lyrics" /> of the song.
    /// </summary>
    public ICollection<Lyrics> Lyrics { get; } = new List<Lyrics>();

    /// <summary>
    ///     Gets the <see cref="Songs.Karaoke" /> of the song.
    /// </summary>
    public ICollection<Karaoke> Karaoke { get; } = new List<Karaoke>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Singer" />s who sang the song.
    /// </summary>
    public ICollection<Singer> Singers { get; } = new List<Singer>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Composer" />s who composed the song.
    /// </summary>
    public ICollection<Composer> Composers { get; } = new List<Composer>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="SongWriter" />s who wrote the song.
    /// </summary>
    public ICollection<SongWriter> SongWriters { get; } = new List<SongWriter>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Work" />s the song appears in.
    /// </summary>
    public ICollection<Work> Sources { get; } = new List<Work>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Collection" />s the song belongs to.
    /// </summary>
    public ICollection<Collection> Collections { get; } = new List<Collection>();

    /// <summary>
    ///     Gets an <see cref="ICollection{T}" /> of <see cref="Tag" />s the song is tagged with.
    /// </summary>
    public ICollection<Tag> Tags { get; } = new List<Tag>();
}