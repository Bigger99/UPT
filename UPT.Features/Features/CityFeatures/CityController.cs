using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Services.City;

namespace UPT.Features.Features.CityFeatures;

/// <summary>
/// Контроллер для User
/// </summary>
public class CityController(ICityService gymService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить город
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var city = await gymService.Get(id);
        return Ok(city);
    }

    /// <summary>
    /// Получить список городов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await gymService.GetAll();
        return Ok(cities);
    }
}
