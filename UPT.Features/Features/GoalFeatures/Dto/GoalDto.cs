using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GoalFeatures.Dto;

public class GoalDto : IDto
{
    public int Id { get; protected set; } = default!;

    public ClientDto Client { get; protected set; } = default!;

    public TrainerDto? TrainerForGoalAchievement { get; protected set; } = default!;

    public TrainingProgram GoalTrainingProgram { get; protected set; } = default!;

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

    static GoalDto()
    {
        TypeAdapterConfig<Goal, GoalDto>
            .NewConfig()
            .Map(dest => dest.Client, src => src.Client.Adapt<ClientDto>())
            .Map(dest => dest.TrainerForGoalAchievement, src => src.TrainerForGoalAchievement.Adapt<TrainerDto>());
    }
}
