using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.AutorizationFeatures.Requests;
using UPT.Features.Services.Autorization;

namespace UPT.Features.Features.AutorizationFeatures;

/// <summary>
/// Контроллер для Autorization
/// </summary>
public class AutorizationController(IAutorizationService autorizationService) : BaseController
{
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        await autorizationService.Register(command.EmailAddress, command.PasswordHash);
        return Ok();
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest command)
    {
        var token = await autorizationService.Login(command.EmailAddress, command.PasswordHash);
        return Ok(token);
    }
}
