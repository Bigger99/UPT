using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.ClientFeatures.Dto;

public class ClientDto : IDto
{
    public int Id { get; set; } = default!;
    public UserDto User { get; set; } = default!;
    public int Height { get; set; } = default!;
    public double Weight { get; set; } = default!;
    public double VolumeBreast { get; set; } = default!;
    public double VolumeWaist { get; set; } = default!;
    public double VolumeAbdomen { get; set; } = default!;
    public double VolumeButtock { get; set; } = default!;
    public double VolumeHip { get; set; } = default!;
    public int TrainerId { get; set; } = default!;

    static ClientDto()
    {
        TypeAdapterConfig<Client, ClientDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .Map(dest => dest.TrainerId, src => src.Trainer.Id);
    }
}
