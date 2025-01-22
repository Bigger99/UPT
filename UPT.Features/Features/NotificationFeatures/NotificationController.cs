using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.NotificationFeatures.Requests;
using UPT.Features.Services.Notification;

namespace UPT.Features.Features.NotificationFeatures;

/// <summary>
/// Контроллер для Notification
/// </summary>
public class NotificationController(INotificationService notificationService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить список прочитанных уведомлений клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCkecked([FromQuery] int userId)
    {
        var notificationList = await notificationService.GetCkecked(userId);
        return Ok(notificationList);
    }

    /// <summary>
    /// Получить список не прочитанных уведомлений клиента
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUnCkecked([FromQuery] int userId)
    {
        var notificationList = await notificationService.GetUnCkecked(userId);
        return Ok(notificationList);
    }


    /// <summary>
    /// Добавить уведомление
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationCommand command)
    {
        var notification = await notificationService.Create(command.Name, command.Text, command.UserId);
        return Ok(notification);
    }

    /// <summary>
    /// Пометить уведомление прочитанным
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> SetChecked([FromQuery] int notificationId)
    {
        await notificationService.SetChecked(notificationId);
        return Ok();
    }

    /// <summary>
    /// Удалить уведомление
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int notificationId)
    {
        await notificationService.Delete(notificationId);
        return Ok();
    }
}
