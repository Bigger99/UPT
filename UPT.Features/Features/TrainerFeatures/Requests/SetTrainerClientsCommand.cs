using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.TrainerFeatures.Requests;

public class SetTrainerClientsCommand
{
    [Required] public int TrainerId { get; set; } = default!;
    [Required] public List<int> ClientIds { get; set; } = default!;
}
