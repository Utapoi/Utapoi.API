using FluentResults;
using MediatR;
using Utapoi.Application.Common;

namespace Utapoi.Application.Singers.Requests.GetSingersForAdmin;

public static partial class GetSingersForAdmin
{
    /// <summary>
    ///     Represents a request to get a paginated list of singers.
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
        ///     Gets the number of items to skip.
        /// </summary>
        public int Skip { get; init; }

        /// <summary>
        ///     Gets the number of items to take.
        /// </summary>
        public int Take { get; init; }
    }
}