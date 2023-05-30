using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Karaoke.API.Controllers;

/// <summary>
///     The base controller for the application.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    /// <summary>
    ///     Gets the mediator.
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    /// <summary>
    ///     Gets the origin from the request.
    /// </summary>
    /// <returns>
    ///     The origin from the request.
    /// </returns>
    protected string GetOriginFromRequest()
    {
        return $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
    }
}