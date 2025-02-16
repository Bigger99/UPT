using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GymFeatures.Dto;

public class GymTrainerDto : IDto
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

    public double Rating { get; set; } = default!;

    static GymTrainerDto()
    {
        TypeAdapterConfig<Trainer, GymTrainerDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
