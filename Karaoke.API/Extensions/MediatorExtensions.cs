using FluentResults;
using Karaoke.Application.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Karaoke.Application.Common.Extensions;
using Karaoke.Application.Common;

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
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>
    ///     An <see cref="IActionResult" />.
    /// </returns>
    public static async Task<IActionResult> ProcessRequestAsync<T>(this ISender mediator, IRequest<Result<T>> request, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(request, cancellationToken);

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

    /// <summary>
    ///     Processes the command and returns an <see cref="IActionResult" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the result.
    /// </typeparam>
    /// <param name="mediator">
    ///     The <see cref="ISender" />.
    /// </param>
    /// <param name="command">
    ///     The <see cref="ICommand{T}" />.
    /// </param>
    /// <returns>
    ///     An <see cref="IActionResult" />.
    /// </returns>
    public static async Task<IActionResult> ProcessCommandAsync<T>(this ISender mediator, ICommand<Result<T>> command)
    {
        var result = await mediator.Send(command);

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

    /// <summary>
    ///    Processes the command and returns an <see cref="IActionResult" />.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="mediator">The <see cref="ISender"/>.</param>
    /// <param name="command">The <see cref="IRequest{T}"/>.</param>
    /// <param name="controllerName">The name of the controller that initiated the command.</param>
    /// <param name="actionName">The name of the action that initiated the command.</param>
    /// <returns>
    ///    An <see cref="IActionResult" />.
    /// </returns>
    public static async Task<IActionResult> ProcessCreateCommandAsync<T>(this ISender mediator, ICommand<Result<T>> command, string controllerName, string actionName)
    {
        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.Select(x => x.Message));
        }

        return new CreatedAtActionResult(actionName, controllerName, new { id = result.Value }, result.Value);
    }
}