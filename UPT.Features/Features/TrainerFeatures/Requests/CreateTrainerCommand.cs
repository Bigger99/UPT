using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.TrainerFeatures.Requests;

public class CreateTrainerCommand
{
    [Required] public int UserId { get; set; } = default!;

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

    [Required] public List<TrainingProgram> TrainingPrograms { get; set; } = default!;

    [Required] public int GymId { get; set; } = default!;

    public string? Description { get; set; } = default!;
}
