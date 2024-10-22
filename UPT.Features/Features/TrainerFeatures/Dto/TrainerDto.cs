using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.TrainerFeatures.Dto;

public class TrainerDto : IDto
{
    public UserDto User { get; protected set; } = default!;

    /// <summary>
    /// Стаж работы
    /// </summary>
    public int Experience { get; protected set; } = default!;

    /// <summary>
    /// Наличие медицинского образования
    /// </summary>
    public bool MedicGrade { get; protected set; } = default!;

    /// <summary>
    /// Работа с травмами
    /// </summary>
    public bool WorkInjuries { get; protected set; } = default!;

    /// <summary>
    /// Работас спортсменами
    /// </summary>
    public bool WorkSportsmens { get; protected set; } = default!;

    /// <summary>
    /// Восстановление опорно-двигательного аппарата
    /// </summary>
    public TrainingProgram TrainingProgram { get; protected set; } = default!;

    public List<int> Clients { get; protected set; } = default!;

    public int GymId { get; protected set; } = default!;

    static TrainerDto()
    {
        TypeAdapterConfig<Trainer, TrainerDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .Map(dest => dest.GymId, src => src.Gym.Id)
            .Map(dest => dest.Clients, src => src.Clients.Select(x => x.Id));
    }
}
