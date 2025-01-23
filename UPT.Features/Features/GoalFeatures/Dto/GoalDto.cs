using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GoalFeatures.Dto;

public class GoalDto : IDto
{
    public int Id { get; set; } = default!;

    public ClientDto Client { get; set; } = default!;

    public TrainerDto? TrainerForGoalAchievement { get; set; } = default!;

    public TrainingProgram GoalTrainingProgram { get; set; } = default!;

    public double CurrentWeight { get; set; } = default!;

    /// <summary>
    /// Желаемый вес
    /// </summary>
    public double DesiredWeight { get; set; } = default!;

    /// <summary>
    /// Срок достижения результата
    /// </summary>
    public Deadline DeadlineForResult { get; set; } = default!;

    /// <summary>
    /// Дни тренировок
    /// </summary>
    public List<DayOfWeek> DaysOfWeekForTraining { get; set; } = default!;

    /// <summary>
    /// Время тренировок
    /// </summary>
    public TimeOfDay TimeForTraining { get; set; } = default!;

    /// <summary>
    /// Имеются ли травмы
    /// </summary>
    public bool HasInjuries { get; set; } = false;

    static GoalDto()
    {
        TypeAdapterConfig<Goal, GoalDto>
            .NewConfig()
            .Map(dest => dest.Client, src => src.Client.Adapt<ClientDto>())
            .Map(dest => dest.TrainerForGoalAchievement, src => src.TrainerForGoalAchievement.Adapt<TrainerDto>());
    }
}
