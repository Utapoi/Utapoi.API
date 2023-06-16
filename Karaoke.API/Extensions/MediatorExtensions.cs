using FluentResults;
using Karaoke.Application.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Karaoke.Application.Common.Extensions;

namespace Karaoke.API.Extensions;

/// <summary>
///     MediatR extensions.
/// </summary>
public static class MediatorExtensions
{
    /// <summary>
    ///     Processes the request and returns an <see cref="IActionResult" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the result.
    /// </typeparam>
    /// <param name="mediator">
    ///     The <see cref="ISender" />.
    /// </param>
    /// <param name="request">
    ///     The <see cref="IRequest{T}" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IActionResult" />.
    /// </returns>
    public static async Task<IActionResult> ProcessAsync<T>(this ISender mediator, IRequest<Result<T>> request)
    {
        var result = await mediator.Send(request);

        if (result.HasError<EntityNotFoundError>())
        {
            return new NotFoundObjectResult(result.GetValueFromError<EntityNotFoundError>("Id"));
        }

        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.Select(x => x.Message));
        }

        return new OkObjectResult(result.Value);
    }
}