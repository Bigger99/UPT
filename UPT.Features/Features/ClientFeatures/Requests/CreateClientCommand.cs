using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.ClientFeatures.Requests;

public class CreateClientCommand
{
    [Required] public int UserId { get; set; } = default!;

    public int Height { get; set; } = default!;
    public double Weight { get; set; } = default!;
    public double VolumeBreast { get; set; } = default!;
    public double VolumeWaist { get; set; } = default!;
    public double VolumeAbdomen { get; set; } = default!;
    public double VolumeButtock { get; set; } = default!;
    public double VolumeHip { get; set; } = default!;

    [Required] public TrainingProgram TrainingPrograms { get; set; } = default!;
}
