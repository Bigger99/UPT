using Microsoft.AspNetCore.Mvc;

namespace UPT.Features.Base;

[ApiController]
[Route("api/web/[controller]/[action]")]
internal class BaseController : ControllerBase
{
}
