using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;

namespace UPT.Features.Features.User;

/// <summary>
/// Контроллер для User
/// </summary>
public class UserController : BaseController
{
    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(1);
    }
}
