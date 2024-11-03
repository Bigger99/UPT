using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;
using UPT.Features.Features.GoalFeatures.Requests;
using UPT.Features.Features.NewsFeatures.Requests;
using UPT.Features.Services.Goal;
using UPT.Features.Services.News;

namespace UPT.Features.Features.GoalFeatures;

/// <summary>
/// Контроллер для News
/// </summary>
public class GoalController(IGoalService goalService) : BaseAuthorizeController
{
    /// <summary>
    /// Получить цель
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int clientId)
    {
        var goal = await goalService.Get(clientId);
        return Ok(goal);
    }

    /// <summary>
    /// Получить список целей пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllClientGoals([FromQuery] int clientId)
    {
        var goals = await goalService.GetAllClientGoals(clientId);
        return Ok(goals);
    }

    /// <summary>
    /// Добавить цель
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalCommand command)
    {
        var goal = await goalService.Create(command.ClientId, command.TrainingProgram, command.CurrentWeight, command.DesiredWeight, command.DeadlineForResult,
            command.DaysOfWeekForTraining, command.TimeForTraining, command.HasInjuries);
        return Ok(goal);
    }

    /// <summary>
    /// Изменить цель
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateGoalCommand command)
    {
        var goal = await goalService.Update(command.GoalId, command.TrainingProgram, command.CurrentWeight, command.DesiredWeight, command.DeadlineForResult,
            command.DaysOfWeekForTraining, command.TimeForTraining, command.HasInjuries);
        return Ok(goal);
    }

    /// <summary>
    /// Назначить тренера для цели
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> SetTrainer([FromBody] SetGoalTrainerCommand command)
    {
        var goal = await goalService.SetTrainer(command.GoalId, command.TrainerId);
        return Ok(goal);
    }

    /// <summary>
    /// Удалить цель
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int goalId)
    {
        await goalService.Delete(goalId);
        return Ok();
    }
}
