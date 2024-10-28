using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.FeedbackFeatures.Requests;
using UPT.Features.Services.Feedback;

namespace UPT.Features.Features.FeedbackFeatures;

/// <summary>
/// Контроллер для Feedback
/// </summary>
public class FeedbackController(IFeedbackService feedbackService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить список отзывов на тренера
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int trainerId)
    {
        var favoriteList = await feedbackService.Get(trainerId);
        return Ok(favoriteList);
    }

    /// <summary>
    /// Добавить отзыв тренеру
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddFeedbackCommand command)
    {
        var favoriteList = await feedbackService.Add(command.ClientId, command.TrainerId, command.Rating, command.Text);
        return Ok(favoriteList);
    }

    /// <summary>
    /// Удалить отзыв тренеру
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFeedbackCommand command)
    {
        await feedbackService.Delete(command.ClientId, command.TrainerId);
        return Ok();
    }
}
