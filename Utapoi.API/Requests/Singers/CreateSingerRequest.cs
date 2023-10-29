using Utapoi.Application.Common.Requests;

namespace Utapoi.API.Requests.Singers;

/// <summary>
///     Represents a request to create a singer.
/// </summary>
public class CreateSingerRequest
{
    /// <summary>
    ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="LocalizedStringRequest" />s representing the names of
    ///     the
    ///     singer.
    /// </summary>
    public IEnumerable<LocalizedStringRequest> Names { get; set; } = new List<LocalizedStringRequest>();

    /// <summary>
    ///     Gets or sets an <see cref="ICollection{T}" /> of <see cref="LocalizedStringRequest" />s representing the nicknames
    ///     of the
    ///     singer.
    /// </summary>
    public IEnumerable<LocalizedStringRequest> Nicknames { get; set; } = new List<LocalizedStringRequest>();

    /// <summary>
    ///     Gets or sets the birthday of the singer.
    /// </summary>
    public DateTime? Birthday { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     Gets or sets the profile picture of the singer.
    /// </summary>
    public byte[] Image { get; set; } = Array.Empty<byte>();

    /// <summary>
    ///     Gets or sets the type of the image of the singer.
    /// </summary>
    public string ImageType { get; set; } = string.Empty;
}