using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Jwt;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.User;

public class UserService(UPTDbContext dbContext, IJwtProvider jwtProvider) : IUserService
{
    public async Task<UserDto> Get(int id)
    {
        var user = await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        return user.Adapt<UserDto>();
    }

    public async Task<UserDto> GetByEmail(string emailAddress)
    {
        var emailAddressLower = emailAddress.ToLower();

        var user = await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.EmailAddress.ToLower() == emailAddressLower) ?? throw new BackendException("User not found");

        return user.Adapt<UserDto>();
    }

    public async Task<UserDto> Update(int id, string name, string phoneNumber, string emailAddress, int cityId,
        Gender gender, bool isNotificationEnable, bool isEmailNotificationEnable, string? avatar)
    {
        var user = await dbContext.Users
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        var city = await dbContext.Cities.FirstOrDefaultAsync(x => x.Id == cityId) 
            ?? throw new BackendException("City not found");

        user.EditUserData(name, phoneNumber, emailAddress, city, gender, isNotificationEnable, isEmailNotificationEnable, avatar);
        await dbContext.SaveChangesAsync();
        return user.Adapt<UserDto>();
    }

    public async Task Delete(int id)
    {
        var user = await dbContext.Users
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        user.Delete();
        jwtProvider.DeleteUser(id);
        await dbContext.SaveChangesAsync();
    }

    public async Task SetEmailConfirmed(int id)
    {
        var user = await dbContext.Users
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        user.ConfirmeEmail();
        await dbContext.SaveChangesAsync();
    }
}
