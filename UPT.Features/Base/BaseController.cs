using Microsoft.AspNetCore.Mvc;

namespace UPT.Features.Base;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
[Route("api/web/[controller]/[action]")]
public class BaseController : ControllerBase
{
}
