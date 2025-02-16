using Mapster;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.UserFeatures.Dto;

public class UserDto : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public CityDto? City { get; set; } = default!;
    public Gender? Gender { get; set; } = default!;
    public string? Avatar { get; set; } = default!;
    public bool IsNotificationEnable { get; set; } = true;
    public bool IsEmailNotificationEnable { get; set; } = false;

    static UserDto()
    {
        TypeAdapterConfig<Domain.Entities.User, UserDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City != null ? src.City : null);
    }
}
