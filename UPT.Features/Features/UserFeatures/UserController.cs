using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using UPT.Features.Base;
using UPT.Features.Features.UserFeatures.Requests;
using UPT.Features.Services.User;

namespace UPT.Features.Features.UserFeatures;

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
    /// Получить пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByEmail([FromQuery] GetByEmailQuery query)
    {
        var user = await userService.GetByEmail(query.EmailAddress);
        return Ok(user);
    }

    /// <summary>
    /// Изменить пользователя
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var user = await userService.Update(command.Id, command.Name, command.PhoneNumber, command.EmailAddress, command.CityId, 
            command.Gender, command.IsNotificationEnable, command.IsEmailNotificationEnable, command.Avatar);
        return Ok(user);
    }

    /// <summary>
    /// Подтвердить почту пользователя
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> EmailConfirmed([FromBody] ConfirmeEmailCommand command)
    {
        await userService.SetEmailConfirmed(command.UserId);
        return Ok();
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
