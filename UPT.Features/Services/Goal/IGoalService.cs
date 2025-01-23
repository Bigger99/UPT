using UPT.Features.Features.GoalFeatures.Dto;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Services.Goal;

public interface IGoalService
{
    Task<GoalDto> Create(int clientId, TrainingProgram trainingProgram, double currentWeight, double desiredWeight, 
        Deadline deadlineForResult, List<int> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries);
    Task Delete(int goalId);
    Task<GoalDto?> Get(int goalId);
    Task<List<GoalDto>?> GetAllClientGoals(int clientId);
    Task<GoalDto> SetTrainer(int goalId, int trainerId);
    Task<GoalDto> Update(int goalId, TrainingProgram trainingProgram, double currentWeight, double desiredWeight, 
        Deadline deadlineForResult, List<int> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries);
}