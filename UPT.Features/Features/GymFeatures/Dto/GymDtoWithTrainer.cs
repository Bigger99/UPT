using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.GymFeatures.Dto;

public class GymDtoWithTrainer : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public TimeOnly OpenTime { get; set; } = default!;
    public TimeOnly CloseTime { get; set; } = default!;
    public string Location { get; set; } = default!;
    public CityDto City { get; set; } = default!;

    public List<GymTrainerDto> Trainers { get; set; } = default!;

    static GymDtoWithTrainer()
    {
        TypeAdapterConfig<Gym, GymDtoWithTrainer>
            .NewConfig()
            .Map(dest => dest.City, src => src.City.Adapt<CityDto>())
            .Map(dest => dest.Trainers, src => src.Trainers);
    }
}
