﻿using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Services.Gym;

namespace UPT.Features.Features.User;

/// <summary>
/// Контроллер для User
/// </summary>
public class GymController(IGymService gymService) : BaseController
{
    /// <summary>
    /// Получить зал
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var gyms = await gymService.Get(id);
        return Ok(gyms);
    }

    /// <summary>
    /// Получить все залы
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var gyms = await gymService.GetAll();
        return Ok(gyms);
    }
}