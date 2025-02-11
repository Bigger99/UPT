﻿using Mapster;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.UserFeatures.Dto;

public class UserDto : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public string? City { get; set; } = default!;
    public Gender? Gender { get; set; } = default!;
    public string? Avatar { get; set; } = default!;

    static UserDto()
    {
        TypeAdapterConfig<Domain.Entities.User, UserDto>
            .NewConfig()
            .Map(dest => dest.City, src => src.City != null ? src.City.Name : string.Empty);
    }
}
