using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.PaymentFeatures.Requests;
using UPT.Features.Services.Payment;

namespace UPT.Features.Features.PaymentFeatures;

/// <summary>
/// Контроллер для Feedback
/// </summary>
public class PaymentController(IPaymentService paymentService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить список оплат пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int userId)
    {
        var favoriteList = await paymentService.Get(userId);
        return Ok(favoriteList);
    }

    /// <summary>
    /// Добавить оплату
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddPaymentCommand command)
    {
        var favoriteList = await paymentService.Add(command.UserId, command.Title, command.Amount);
        return Ok(favoriteList);
    }

    /// <summary>
    /// Удалить оплату
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int paymentId)
    {
        await paymentService.Delete(paymentId);
        return Ok();
    }
}
