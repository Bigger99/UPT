using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.ClientFeatures.Dto;

public class ClientWithGoalsDto : IDto
{
    public int Id { get; set; } = default!;
    public UserDto User { get; set; } = default!;
    public int Height { get; set; } = default!;
    public double Weight { get; set; } = default!;
    public double VolumeBreast { get; set; } = default!;
    public double VolumeWaist { get; set; } = default!;
    public double VolumeAbdomen { get; set; } = default!;
    public double VolumeButtock { get; set; } = default!;
    public double VolumeHip { get; set; } = default!;
    public int TrainerId { get; set; } = default!;

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
    public int Id { get; set; } = default!;

    public TrainingProgram TrainingProgram { get; set; } = default!;

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

    static SubGoalDto()
    {
        TypeAdapterConfig<Goal, SubGoalDto>
            .NewConfig();
    }
}