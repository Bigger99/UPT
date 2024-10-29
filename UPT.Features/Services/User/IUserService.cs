using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Services.User;

public interface IUserService
{
    Task Delete(int id);
    Task<UserDto> Get(int id);
    Task SetEmailConfirmed(int id);
    Task<UserDto> Update(int id, string name, string phoneNumber, string emailAddress, Domain.Entities.City city, Gender gender, bool isNotificationEnable, bool isEmailNotificationEnable);
}