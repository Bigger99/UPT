using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.GoalFeatures.Requests;

public class UpdateGoalCommand
{
    [Required]
    public int GoalId { get; set; }

    [Required]
    public TrainingProgram TrainingProgram { get; set; }

    [Required]
    public double CurrentWeight { get; set; }

    [Required]
    public double DesiredWeight { get; set; }

    public Deadline DeadlineForResult { get; set; }

    [Required]
    public List<int> DaysOfWeekForTraining { get; set; } = default!;

    [Required]
    public TimeOfDay TimeForTraining { get; set; }

    public bool HasInjuries { get; set; } = default!;
}
