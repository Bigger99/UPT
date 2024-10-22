using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.Trainer.Requests;
using UPT.Features.Services.User;
using UPT.Infrastructure.Models;

namespace UPT.Features.Features.User;

/// <summary>
/// Контроллер для Trainer
/// </summary>
public class TrainerController(ITrainerService trainerService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить тренера
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var user = await trainerService.Get(id);
        return Ok(user);
    }

    /// <summary>
    /// Получить тренера
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery] int id)
    {
        var trainer = await trainerService.GetByUserId(id);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить тренера
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GetFiltered([FromBody] PagedFilterQuery<TrainerRequest> request)
    {
        var trainer = await trainerService.GetFilteredTrainers(request);
        return Ok(trainer);
    }

    /// <summary>
    /// Создать тренера
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTrainerCommand command)
    {
        var trainer = await trainerService.Create(command.UserId, command.Experience, command.MedicGrade, command.WorkInjuries, command.WorkSportsmens, command.TrainingPrograms, command.GymId);
        return Ok(trainer);
    }

    /// <summary>
    /// Изменить тренера
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTrainerCommand command)
    {
        var trainer = await trainerService.Update(command.TrainerId, command.Experience, command.MedicGrade, command.WorkInjuries, command.WorkSportsmens, command.TrainingPrograms, command.ClientsIds, command.GymId);
        return Ok(trainer);
    }

    /// <summary>
    /// Удалить тренера
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await trainerService.Delete(id);
        return Ok();
    }
}
