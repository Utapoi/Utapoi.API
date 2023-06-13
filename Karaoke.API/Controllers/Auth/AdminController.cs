using Karaoke.Core.Common;
using Microsoft.AspNetCore.Authorization;

namespace Karaoke.API.Controllers.Auth;

/// <summary>
///     The controller for admin actions.
/// </summary>
[Authorize(Roles = Roles.Admin)]
public class AdminController : ApiControllerBase
{
}