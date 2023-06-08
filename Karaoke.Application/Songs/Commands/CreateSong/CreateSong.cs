using FluentResults;
using Karaoke.Application.Common.Requests;
using MediatR;

namespace Karaoke.Application.Songs.Commands.CreateSong;

public static class CreateSong
{
    /// <summary>
    ///     Represents a <see cref="IRequest{TResponse}" /> for creating a song.
    /// </summary>
    public record Command : IRequest<Result<string>>
    {
        /// <summary>
        ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="LocalizedStringRequest" />s representing the titles of
        ///     the song.
        /// </summary>
        public IEnumerable<LocalizedStringRequest> Titles { get; set; } = new List<LocalizedStringRequest>();

        /// <summary>
        ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="string" />s representing the singers of the song.
        /// </summary>
        public IEnumerable<string> Singers { get; set; } = new List<string>();

        /// <summary>
        ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="string" />s representing the composers of the song.
        /// </summary>
        public IEnumerable<string> Albums { get; set; } = new List<string>();

        /// <summary>
        ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="string" />s representing the tags of the song.
        /// </summary>
        public IEnumerable<string> Tags { get; set; } = new List<string>();

        /// <summary>
        ///     Gets or sets the release date of the song.
        /// </summary>
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

        /// <summary>
        ///     Gets or sets the thumbnail.
        /// </summary>
        public FileRequest Thumbnail { get; set; } = new();

        /// <summary>
        ///     Gets or sets the voice file.
        /// </summary>
        public FileRequest VoiceFile { get; set; } = new();

        /// <summary>
        ///     Gets or sets the instrumental file.
        /// </summary>
        public FileRequest InstrumentalFile { get; set; } = new();

        /// <summary>
        ///     Gets or sets the preview file.
        /// </summary>
        public FileRequest PreviewFile { get; set; } = new();

        /// <summary>
        ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="LocalizedFileRequest" />s representing the karaoke
        ///     files.
        /// </summary>
        public IEnumerable<LocalizedFileRequest> KaraokeFiles { get; set; } = new List<LocalizedFileRequest>();
    }

    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ISongsService _songsService;

        public Handler(ISongsService songsService)
        {
            _songsService = songsService;
        }

        public async Task<Result<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            var songId = await _songsService.CreateAsync(command, cancellationToken);

            return Result.Ok(songId);
        }
    }
}