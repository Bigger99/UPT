using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.TrainerFeatures.Requests;

public class FilteredTrainerRequest
{
    [Required] public TrainingProgram TrainingProgram { get; set; } = default!;

    public int? GymId { get; set; } = default!;
}
