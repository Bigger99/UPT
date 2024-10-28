using Mapster;
using UPT.Domain.Entities;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.CityFeatures.Dto;

public class CityDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;

    static CityDto()
    {
        TypeAdapterConfig<City, CityDto>
            .NewConfig();
    }
}
