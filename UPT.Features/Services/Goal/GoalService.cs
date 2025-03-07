﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.GoalFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Goal;

public class GoalService(UPTDbContext dbContext) : IGoalService
{
    public async Task<GoalDto?> Get(int goalId)
    {
        var goal = await dbContext.Goals
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.TrainerForGoalAchievement)
                .ThenInclude(x => x!.User)
                    .ThenInclude(x => x.City)
            .Include(x => x.TrainerForGoalAchievement)
                .ThenInclude(x => x!.User)
             .Include(x => x.TrainerForGoalAchievement)
                .ThenInclude(x => x!.Gym)
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == goalId)
             ?? throw new BackendException("Goal not found");

        return goal.Adapt<GoalDto>();
    }

    public async Task<List<GoalDto>?> GetAllClientGoals(int clientId)
    {
        var goals = await dbContext.Goals
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.TrainerForGoalAchievement)
               .ThenInclude(x => x!.User)
                    .ThenInclude(x => x.City)
            .Include(x => x.TrainerForGoalAchievement)
                .ThenInclude(x => x!.User)
             .Include(x => x.TrainerForGoalAchievement)
                .ThenInclude(x => x!.Gym)
            .Include(x => x.Client)
            .Where(x => x.Client.Id == clientId)
            .ToListAsync();

        return goals.Select(x => x.Adapt<GoalDto>()).ToList();
    }

    public async Task<GoalDto> Create(int clientId, TrainingProgram trainingProgram, double currentWeight, double desiredWeight,
        Deadline deadlineForResult, List<int> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var daysOfWeek = daysOfWeekForTraining
            .Where(value => Enum.IsDefined(typeof(DayOfWeek), value)) // Проверка, что значение есть в перечислении
            .Select(value => (DayOfWeek)value) // Преобразование int в DayOfWeek
            .ToList();

        var goal = new Domain.Entities.Goal(client, trainingProgram, currentWeight, desiredWeight,
        deadlineForResult, daysOfWeek, timeForTraining, hasInjuries);
        await dbContext.Goals.AddAsync(goal);
        await dbContext.SaveChangesAsync();

        return goal.Adapt<GoalDto>();
    }

    public async Task<GoalDto> Update(int goalId, TrainingProgram trainingProgram, double currentWeight, double desiredWeight,
        Deadline deadlineForResult, List<int> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries)
    {
        var goal = await dbContext.Goals
            .FirstOrDefaultAsync(x => x.Id == goalId) ?? throw new BackendException("Goal not found");

        var daysOfWeek = daysOfWeekForTraining
            .Where(value => Enum.IsDefined(typeof(DayOfWeek), value)) // Проверка, что значение есть в перечислении
            .Select(value => (DayOfWeek)value) // Преобразование int в DayOfWeek
            .ToList();

        goal.UpdateGoal(trainingProgram, currentWeight, desiredWeight,
        deadlineForResult, daysOfWeek, timeForTraining, hasInjuries);

        await dbContext.SaveChangesAsync();

        return goal.Adapt<GoalDto>();
    }

    public async Task<GoalDto> SetTrainer(int goalId, int trainerId)
    {
        var goal = await dbContext.Goals
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == goalId) ?? throw new BackendException("Goal not found");

        var trainer = await dbContext.Trainers
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        goal.SetTrainer(trainer);

        await dbContext.SaveChangesAsync();

        return goal.Adapt<GoalDto>();
    }

    public async Task Delete(int goalId)
    {
        var goal = await dbContext.Goals
            .FirstOrDefaultAsync(x => x.Id == goalId) ?? throw new BackendException("Goal not found");

        if (goal is null)
        {
            return;
        }

        dbContext.Goals.Remove(goal);
        await dbContext.SaveChangesAsync();
    }
}
