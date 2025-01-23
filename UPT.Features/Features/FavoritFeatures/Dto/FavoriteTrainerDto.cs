using Mapster;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.FavoritFeatures.Dto;

public class FavoriteTrainerDto : IDto
{
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
    /// Программа тренировок
    /// </summary>
    public List<TrainingProgram> TrainingPrograms { get; set; } = default!;

    public int GymId { get; set; } = default!;

    public string Description { get; set; } = default!;

    static FavoriteTrainerDto()
    {
        TypeAdapterConfig<Trainer, FavoriteTrainerDto>
            .NewConfig()
            .Map(dest => dest.GymId, src => src.Gym.Id);
    }
}
