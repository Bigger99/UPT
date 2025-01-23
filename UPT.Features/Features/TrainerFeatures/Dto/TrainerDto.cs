using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.TrainerFeatures.Dto;

public class TrainerDto : IDto
{
    public int Id { get; set; } = default!;

    public UserDto User { get; set; } = default!;

    /// <summary>
    /// Стаж работы
    /// </summary>
    public int Experience { get; set; } = default!;

    /// <summary>
    /// Наличие медицинского образования
    /// </summary>
    public bool MedicGrade { get; set; } = default!;

    /// <summary>
    /// Работа с травмами
    /// </summary>
    public bool WorkInjuries { get; set; } = default!;

    /// <summary>
    /// Работас спортсменами
    /// </summary>
    public bool WorkSportsmens { get; set; } = default!;

    /// <summary>
    /// Восстановление опорно-двигательного аппарата
    /// </summary>
    public List<TrainingProgram> TrainingPrograms { get; set; } = default!;

    public List<int> Clients { get; set; } = default!;

    public int GymId { get; set; } = default!;

    public string Description { get; set; } = default!;
    public double Rating { get; set; } = default!;
    public int? DialogCount { get; set; } = default!;

    static TrainerDto()
    {
        TypeAdapterConfig<Trainer, TrainerDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .Map(dest => dest.GymId, src => src.Gym.Id)
            .Map(dest => dest.Clients, src => src.Clients.Select(x => x.Id));
    }
}
