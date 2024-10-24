﻿using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.FavoritFeatures.Requests;
using UPT.Features.Services.Favorit;

namespace UPT.Features.Features.FavoritFeatures;

/// <summary>
/// Контроллер для User
/// </summary>
public class FavoritController(IFavoritService favoritService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить список избранного пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int clientId)
    {
        var user = await favoritService.Get(clientId);
        return Ok(user);
    }

    /// <summary>
    /// Добавить в список избранного
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Add([FromBody] AddFavoritCommand command)
    {
        var user = await favoritService.Add(command.ClientId, command.TrainerId);
        return Ok(user);
    }

    /// <summary>
    /// Удалить из списка избранного
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFavoritCommand command)
    {
        await favoritService.Delete(command.ClientId, command.TrainerId);
        return Ok();
    }
}
