using Microsoft.AspNetCore.Authorization;

namespace UPT.Features.Base;

/// <summary>
/// BaseAuthorizeControllery
/// </summary>
[Authorize]
public class BaseAuthorizeController : BaseController
{
}
