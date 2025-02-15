using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.ChatFeatures.Requests;
using UPT.Features.Services.Chat;

namespace UPT.Features.Features.ChatFeatures;

/// <summary>
/// Контроллер для Feedback
/// </summary>
public class ChatController(IChatService chatService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить историю диалога
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetHistory([FromQuery] int senderUserId, [FromQuery] int recipientUserId)
    {
        var historyChat = await chatService.GetHistory(senderUserId, recipientUserId);
        return Ok(historyChat);
    }

    /// <summary>
    /// Добавить сообщение
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddChatCommand command)
    {
        await chatService.Add(command.SenderId, command.RecipientId, command.Message);
        return Ok();
    }

    /// <summary>
    /// Удалить сообщение
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int messageId)
    {
        await chatService.Delete(messageId);
        return Ok();
    }
}
