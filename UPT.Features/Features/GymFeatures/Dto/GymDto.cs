using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GymFeatures.Dto;

public class GymDto : IDto
{
    public string? Name { get; protected set; } = default!;
    public TimeOnly OpenTime { get; protected set; } = default!;
    public TimeOnly CloseTime { get; protected set; } = default!;
    public string Location { get; protected set; } = default!;
    public CityDto City { get; protected set; } = default!;

    public List<int> Trainers { get; protected set; } = default!;

    static GymDto()
    {
        TypeAdapterConfig<Gym, GymDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City.Adapt<CityDto>())
            .Map(dest => dest.Trainers, src => src.Trainers.Select(x => x.Id));
    }
}
