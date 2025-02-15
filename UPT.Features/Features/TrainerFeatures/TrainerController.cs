using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.TrainerFeatures.Requests;
using UPT.Features.Services.Trainer;
using UPT.Infrastructure.Models;

namespace UPT.Features.Features.TrainerFeatures;

/// <summary>
/// Контроллер для Trainer
/// </summary>
public class TrainerController(ITrainerService trainerService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить тренера
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int trainerId)
    {
        var trainer = await trainerService.Get(trainerId);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить всех тренеров
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trainers = await trainerService.GetAll();
        return Ok(trainers);
    }

    /// <summary>
    /// Получить тренера
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery] int userId)
    {
        var trainer = await trainerService.GetByUserId(userId);
        return Ok(trainer);
    }

    /// <summary>
    /// Может ли тренер публиковать новости
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> AccessToPublishNews([FromQuery] int trainerId)
    {
        var trainer = await trainerService.AccessToPublishNews(trainerId);
        return Ok(trainer);
    }

    /// <summary>
    /// Получить список отфильтрованных тренеров
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GetFiltered([FromBody] PagedFilterQuery<FilteredTrainerRequest> request)
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
        var trainer = await trainerService.Create(command.UserId, command.Experience, command.MedicGrade, command.WorkInjuries, command.WorkSportsmens, command.TrainingPrograms, command.GymId, command.Description);
        return Ok(trainer);
    }

    /// <summary>
    /// Изменить тренера
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTrainerCommand command)
    {
        var trainer = await trainerService.Update(command.TrainerId, command.Experience, command.MedicGrade, command.WorkInjuries, command.WorkSportsmens, command.TrainingPrograms, command.GymId, command.Description);
        return Ok(trainer);
    }

    /// <summary>
    /// Назначить тренеру клиентов
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> SetClients([FromBody] SetTrainerClientsCommand command)
    {
        var trainer = await trainerService.SetClients(command.TrainerId, command.ClientIds);
        return Ok(trainer);
    }

    /// <summary>
    /// Списоть счётчик доступных откликов
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> DialogCountDecremen([FromQuery] int trainerId)
    {
        var trainer = await trainerService.DialogCountDecremen(trainerId);
        return Ok(trainer);
    }

    /// <summary>
    /// Удалить тренера
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int trainerId)
    {
        await trainerService.Delete(trainerId);
        return Ok();
    }
}
