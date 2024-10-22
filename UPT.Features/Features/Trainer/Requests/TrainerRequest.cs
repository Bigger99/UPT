using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.Trainer.Requests;

public class TrainerRequest
{
    [Required] public TrainingProgram TrainingProgram { get; set; } = default!;

    public int? GymId { get; set; } = default!;
}
