﻿using UPT.Domain.Entities;
using UPT.Features.Features.User.Dto;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Services.User;

public interface IUserService
{
    Task<UserDto> Get(int id);
    Task Delete(int id);
    Task<UserDto> Update(int id, string name, string phoneNumber, string emailAddress, City city, Role role, Gender gender);
}