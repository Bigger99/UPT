using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.ClientFeatures.Requests;

public class UpdateClientCommand
{
    [Required] public int ClientId { get; set; } = default!;

    public int Height { get; set; } = default!;
    public double Weight { get; set; } = default!;
    public double VolumeBreast { get; set; } = default!;
    public double VolumeWaist { get; set; } = default!;
    public double VolumeAbdomen { get; set; } = default!;
    public double VolumeButtock { get; set; } = default!;
    public double VolumeHip { get; set; } = default!;
}
