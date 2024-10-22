using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.ClientFeatures.Dto;

public class ClientDto : IDto
{
    public UserDto User { get; protected set; } = default!;
    public int Height { get; protected set; } = default!;
    public double Weight { get; protected set; } = default!;
    public double VolumeBreast { get; protected set; } = default!;
    public double VolumeWaist { get; protected set; } = default!;
    public double VolumeAbdomen { get; protected set; } = default!;
    public double VolumeButtock { get; protected set; } = default!;
    public double VolumeHip { get; protected set; } = default!;
    public int TrainerId { get; protected set; } = default!;

    /// <summary>
    /// Программа тренировки выбранная клинтом
    /// </summary>
    public TrainingProgram TrainingProgram { get; protected set; } = default!;

    static ClientDto()
    {
        TypeAdapterConfig<Client, ClientDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .Map(dest => dest.TrainerId, src => src.Trainer.Id);
    }
}
