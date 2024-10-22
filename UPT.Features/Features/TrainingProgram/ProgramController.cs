using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.Program;

public class ProgramController : BaseAuthorizeController
{
    /// <summary>
    /// Получить список тренировочных программ
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        var dict = Enum.GetValues(typeof(TrainingProgram))
               .Cast<TrainingProgram>()
               .ToDictionary(t => (int)t, t => t.ToString());

        return Ok(dict);
    }
}
