using Mapster;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.User.Dto;

public class UserDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;
    public string EmailAddress { get; protected set; } = default!;
    public string? PhoneNumber { get; protected set; } = default!;
    public string? City { get; protected set; } = default!;
    public Role? Role { get; protected set; } = default!;
    public Gender? Gender { get; protected set; } = default!;
    public bool IsDeleted { get; protected set; } = false;

    static UserDto()
    {
        TypeAdapterConfig<Domain.Entities.User, UserDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City != null ? src.City.Name : string.Empty);
    }
}
