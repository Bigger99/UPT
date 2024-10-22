using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.ClientFeatures.Requests;

public class SetClientTrainerCommand
{
    [Required] public int ClientId { get; set; } = default!;
    [Required] public int TrainerId { get; set; } = default!;
}
