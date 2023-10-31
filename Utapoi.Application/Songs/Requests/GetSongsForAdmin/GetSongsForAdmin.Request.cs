using FluentResults;
using MediatR;
using Utapoi.Application.Common;

namespace Utapoi.Application.Songs.Requests.GetSongsForAdmin;

public static partial class GetSongsForAdmin
{
    /// <summary>
    ///     The request for getting all <see cref="Song" />s.
    /// </summary>
    public sealed class Request : IRequest<Result<PaginatedResponse<Response>>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        /// <param name="skip">The number of items to skip.</param>
        /// <param name="take">The number of items to take.</param>
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