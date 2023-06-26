using FluentResults;
using Karaoke.Application.Common;
using MediatR;

namespace Karaoke.Application.Albums.Requests.GetAlbumsForAdmin;

public static partial class GetAlbumsForAdmin
{
    /// <summary>
    ///     The request for getting all <see cref="Album" />s.
    /// </summary>
    public sealed class Request : IRequest<Result<PaginatedResponse<Response>>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        public Request(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public Request()
        {
        }

        /// <summary>
        ///     The number of items to skip.
        /// </summary>
        public int Skip { get; init; }

        /// <summary>
        ///     The number of items to take.
        /// </summary>
        public int Take { get; init; }
    }
}