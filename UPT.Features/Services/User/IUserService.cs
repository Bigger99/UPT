using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Services.User;

public interface IUserService
{
    Task Delete(int id);
    Task<UserDto> Get(int id);
    Task<UserDto> GetByEmail(string emailAddress);
    Task SetEmailConfirmed(int id);
    Task<UserDto> Update(int id, string name, string phoneNumber, string emailAddress, int cityId, Gender gender, 
        bool isNotificationEnable, bool isEmailNotificationEnable, string? avatar);
}