using Karaoke.Application.Common.Requests;
using Karaoke.Core.Entities;

namespace Karaoke.API.Requests.Singers;

/// <summary>
///    Represents a request to get a paginated list of <see cref="Singer" />.
/// </summary>
public sealed class GetSingersRequest : PaginatedRequest
{
}
