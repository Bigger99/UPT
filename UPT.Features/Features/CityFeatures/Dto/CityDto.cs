using Mapster;
using UPT.Domain.Entities;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.CityFeatures.Dto;

public class CityDto : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;

    static CityDto()
    {
        TypeAdapterConfig<City, CityDto>
            .NewConfig();
    }
}
