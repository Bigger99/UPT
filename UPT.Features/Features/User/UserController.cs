using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.User.Requests;
using UPT.Features.Services.User;

namespace UPT.Features.Features.User;

/// <summary>
/// Контроллер для User
/// </summary>
public class UserController(IUserService userService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var user = await userService.Get(id);
        return Ok(user);
    }

    /// <summary>
    /// Изменить пользователя
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var user = await userService.Update(command.Id, command.Name, command.PhoneNumber, command.EmailAddress, command.City, command.Role, command.Gender);
        return Ok(user);
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await userService.Delete(id);
        return Ok();
    }
}
