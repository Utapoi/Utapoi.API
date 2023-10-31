using FluentResults;
using MediatR;
using Utapoi.Application.Common.Requests;

namespace Utapoi.Application.Albums.Commands.CreateAlbum;

/// <summary>
///     Create album command.
/// </summary>
public static class CreateAlbum
{
    /// <summary>
    ///     The command.
    /// </summary>
    public class Command : IRequest<Result<string>>
    {
        /// <summary>
        ///    Gets or sets the titles.
        /// </summary>
        public IEnumerable<LocalizedStringRequest> Titles { get; set; } = new List<LocalizedStringRequest>();

        /// <summary>
        ///    Gets or sets the singers.
        /// </summary>
        public IEnumerable<string> Singers { get; set; } = new List<string>();

        /// <summary>
        ///    Gets or sets the release date.
        /// </summary>
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

        /// <summary>
        ///    Gets or sets the cover file.
        /// </summary>
        public FileRequest? CoverFile { get; set; }
    }

    /// <summary>
    ///     The handler.
    /// </summary>
    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IAlbumsService _albumsService;

        /// <summary>
        ///    Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="albumsService">
        ///    The <see cref="IAlbumsService" />.
        /// </param>
        public Handler(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        /// <inheritdoc />
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var album = await _albumsService.CreateAsync(request, cancellationToken);

            return Result.Ok(album.Id.ToString());
        }
    }
}