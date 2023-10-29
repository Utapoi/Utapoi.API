using Microsoft.AspNetCore.Authorization;
using Utapoi.Core.Common;

namespace Utapoi.API.Controllers.Auth;

/// <summary>
///     The controller for admin actions.
/// </summary>
[Authorize(Roles = Roles.Admin)]
public class AdminController : ApiControllerBase
{
}