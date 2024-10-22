using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.ClientFeatures.Requests;

public class CreateClientCommand
{
    [Required] public int UserId { get; set; } = default!;

    public int Height { get; protected set; } = default!;
    public double Weight { get; protected set; } = default!;
    public double VolumeBreast { get; protected set; } = default!;
    public double VolumeWaist { get; protected set; } = default!;
    public double VolumeAbdomen { get; protected set; } = default!;
    public double VolumeButtock { get; protected set; } = default!;
    public double VolumeHip { get; protected set; } = default!;

    [Required] public TrainingProgram TrainingPrograms { get; set; } = default!;
}
