using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

/// <summary>
/// Цели тренировок клиента
/// </summary>
public class Goal : HasIdBase
{
    public Client Client { get; protected set; } = default!;

    public Trainer? TrainerForGoalAchievement { get; protected set; } = default!;

    public TrainingProgram TrainingProgram { get; protected set; } = default!;

    public double CurrentWeight { get; protected set; } = default!;    

    /// <summary>
    /// Желаемый вес
    /// </summary>
    public double DesiredWeight { get; protected set; } = default!;

    /// <summary>
    /// Срок достижения результата
    /// </summary>
    public Deadline DeadlineForResult { get; protected set; } = default!;

    /// <summary>
    /// Дни тренировок
    /// </summary>
    public List<DayOfWeek> DaysOfWeekForTraining { get; protected set; } = default!;

    /// <summary>
    /// Время тренировок
    /// </summary>
    public TimeOfDay TimeForTraining { get; protected set; } = default!;

    /// <summary>
    /// Имеются ли травмы
    /// </summary>
    public bool HasInjuries { get; protected set; } = false;


    public Goal(Client client, TrainingProgram trainingProgram, double currentWeight, double desiredWeight,
        Deadline deadlineForResult, List<DayOfWeek> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries)
    {
        Client = client;
        TrainingProgram = trainingProgram;
        CurrentWeight = currentWeight;
        DesiredWeight = desiredWeight;
        DeadlineForResult = deadlineForResult;
        DaysOfWeekForTraining = daysOfWeekForTraining;
        TimeForTraining = timeForTraining;
        HasInjuries = hasInjuries;
    }

    public void UpdateGoal(TrainingProgram trainingProgram, double currentWeight, double desiredWeight,
        Deadline deadlineForResult, List<DayOfWeek> daysOfWeekForTraining, TimeOfDay timeForTraining, bool hasInjuries)
    {
        TrainingProgram = trainingProgram;
        CurrentWeight = currentWeight;
        DesiredWeight = desiredWeight;
        DeadlineForResult = deadlineForResult;
        DaysOfWeekForTraining = daysOfWeekForTraining;
        TimeForTraining = timeForTraining;
        HasInjuries = hasInjuries;
    }

    public void SetTrainer(Trainer trainerForGoal)
    {
        TrainerForGoalAchievement = trainerForGoal;
    }

    // for EF
    protected Goal()
    {
        
    }
}
