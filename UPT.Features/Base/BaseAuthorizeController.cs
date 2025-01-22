using Microsoft.AspNetCore.Authorization;

namespace UPT.Features.Base;

/// <summary>
/// BaseAuthorizeController
/// </summary>
[Authorize]
public class BaseAuthorizeController : BaseController
{
}
