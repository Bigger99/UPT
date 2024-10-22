using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.Trainer.Requests;

public class UpdateTrainerCommand
{
    [Required] public int TrainerId { get; set; } = default!;

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

    [Required] public List<int> ClientsIds { get; set; } = default!;

    [Required] public int GymId { get; set; } = default!;
}
