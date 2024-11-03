using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.GoalFeatures.Requests;

public class SetGoalTrainerCommand
{
    [Required]
    public int GoalId { get; set; }

    [Required]
    public int TrainerId { get; set; }
}
