using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.NewsFeatures.Requests;
using UPT.Features.Services.News;

namespace UPT.Features.Features.NewsFeatures;

/// <summary>
/// Контроллер для News
/// </summary>
public class NewsController(INewsService newsService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить новость
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int userId)
    {
        var news = await newsService.Get(userId);
        return Ok(news);
    }

    /// <summary>
    /// Получить список новостей
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var news = await newsService.GetAll();
        return Ok(news);
    }

    /// <summary>
    /// Добавить новость
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddNewsCommand command)
    {
        var news = await newsService.Create(command.Title, command.Text, command.UserId, command.Image);
        return Ok(news);
    }

    /// <summary>
    /// Изменить новость
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNewsCommand command)
    {
        var news = await newsService.Update(command.NewsId, command.Title, command.Text, command.UserId, command.Image);
        return Ok(news);
    }

    /// <summary>
    /// Удалить новость
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int notificationId)
    {
        await newsService.Delete(notificationId);
        return Ok();
    }
}
