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
    /// Восстановить пароль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RestorePassword([FromBody] RestorePasswordCommand command)
    {
        await autorizationService.RestorePassword(command.EmailAddress);
        return Ok();
    }

    /// <summary>
    /// Изменить пароль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> EditPassword([FromBody] EditPasswordCommand command)
    {
        await autorizationService.EditPassword(command.EmailAddress, command.OldPassword, command.NewPassword);
        return Ok();
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest command)
    {
        var token = await autorizationService.Login(command.EmailAddress, command.Password);
        return Ok(token);
    }

    /// <summary>
    /// Обновление access токена на основе refresh токена
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenRequest command)
    {
        var token = await autorizationService.RefreshAccessToken(command.AccessToken);
        return Ok(token);
    }
}
