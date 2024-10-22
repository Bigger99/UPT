using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.ClientFeatures.Requests;

public class ClientRequest
{
    [Required] public TrainingProgram TrainingProgram { get; set; } = default!;
}
