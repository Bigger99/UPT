using Mapster;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.TrainerFeatures.Dto;

public class FavoriteTrainerDto : IDto
{
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
    /// Программа тренировок
    /// </summary>
    public List<TrainingProgram> TrainingPrograms { get; protected set; } = default!;

    public int GymId { get; protected set; } = default!;

    public string Description { get; set; } = default!;

    static FavoriteTrainerDto()
    {
        TypeAdapterConfig<Trainer, FavoriteTrainerDto>
            .NewConfig()
            .Map(dest => dest.GymId, src => src.Gym.Id);
    }
}
