using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.InfrastuctureFeatures;

public class InfrastuctureController : BaseAuthorizeController
{
    /// <summary>
    /// Получить список тренировочных программ
    /// </summary>
    [HttpGet]
    public IActionResult GetTrainingProgram()
    {
        var dict = Enum.GetValues(typeof(TrainingProgram))
               .Cast<TrainingProgram>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }

    /// <summary>
    /// Получить список гендеров
    /// </summary>
    [HttpGet]
    public IActionResult GetGenders()
    {
        var dict = Enum.GetValues(typeof(Gender))
               .Cast<Gender>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }

    /// <summary>
    /// Получить список продаваемых продуктов
    /// </summary>
    [HttpGet]
    public IActionResult GetPurchasedProduct()
    {
        var dict = Enum.GetValues(typeof(PurchasedProduct))
               .Cast<PurchasedProduct>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }

    /// <summary>
    /// Получить список дедлайнов
    /// </summary>
    [HttpGet]
    public IActionResult GetDeadlines()
    {
        var dict = Enum.GetValues(typeof(Deadline))
               .Cast<Deadline>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }

    /// <summary>
    /// Получить список дней недели
    /// </summary>
    [HttpGet]
    public IActionResult GetDaysOfWeek()
    {
        var dict = Enum.GetValues(typeof(DayOfWeek))
               .Cast<DayOfWeek>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }

    /// <summary>
    /// Получить список периодов времени в течении дня
    /// </summary>
    [HttpGet]
    public IActionResult GetTimesOfDay()
    {
        var dict = Enum.GetValues(typeof(TimeOfDay))
               .Cast<TimeOfDay>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }
}
