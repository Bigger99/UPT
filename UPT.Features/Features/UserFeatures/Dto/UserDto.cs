using Mapster;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.UserFeatures.Dto;

public class UserDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;
    public string EmailAddress { get; protected set; } = default!;
    public string? PhoneNumber { get; protected set; } = default!;
    public string? City { get; protected set; } = default!;
    public Gender? Gender { get; protected set; } = default!;
    public byte[]? Avatar { get; protected set; } = default!;

    static UserDto()
    {
        TypeAdapterConfig<Domain.Entities.User, UserDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City != null ? src.City.Name : string.Empty);
    }
}
