using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.ClientFeatures.Dto;

public class ClientWithGoalsDto : IDto
{
    public int Id { get; protected set; } = default!;
    public UserDto User { get; protected set; } = default!;
    public int Height { get; protected set; } = default!;
    public double Weight { get; protected set; } = default!;
    public double VolumeBreast { get; protected set; } = default!;
    public double VolumeWaist { get; protected set; } = default!;
    public double VolumeAbdomen { get; protected set; } = default!;
    public double VolumeButtock { get; protected set; } = default!;
    public double VolumeHip { get; protected set; } = default!;
    public int TrainerId { get; protected set; } = default!;

    /// <summary>
    /// Программа тренировки выбранная клинтом
    /// </summary>
    public TrainingProgram TrainingProgram { get; protected set; } = default!;

    public List<SubGoalDto>? Goals { get; set; } = default!;

    static ClientWithGoalsDto()
    {
        TypeAdapterConfig<Client, ClientWithGoalsDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .Map(dest => dest.TrainerId, src => src.Trainer.Id);
    }
}

public class SubGoalDto
{
    public int Id { get; protected set; } = default!;

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

    static SubGoalDto()
    {
        TypeAdapterConfig<Goal, SubGoalDto>
            .NewConfig();
    }
}