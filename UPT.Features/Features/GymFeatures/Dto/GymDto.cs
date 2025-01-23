using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GymFeatures.Dto;

public class GymDto : IDto
{
    public string? Name { get; set; } = default!;
    public TimeOnly OpenTime { get; set; } = default!;
    public TimeOnly CloseTime { get; set; } = default!;
    public string Location { get; set; } = default!;
    public CityDto City { get; set; } = default!;

    public List<int> Trainers { get; set; } = default!;

    static GymDto()
    {
        TypeAdapterConfig<Gym, GymDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City.Adapt<CityDto>())
            .Map(dest => dest.Trainers, src => src.Trainers.Select(x => x.Id));
    }
}
