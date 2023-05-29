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
}