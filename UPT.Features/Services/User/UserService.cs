using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.User;

public class UserService(UPTDbContext dbContext) : IUserService
{
    public async Task<UserDto> Get(int id)
    {
        var user = await dbContext.Users
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        return user.Adapt<UserDto>();
    }

    public async Task<UserDto> Update(int id, string name, string phoneNumber, string emailAddress, Domain.Entities.City city,
        Gender gender, bool isNotificationEnable, bool isEmailNotificationEnable)
    {
        var user = await dbContext.Users
            .Include(x => x.City)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("User not found");

        user.EditUserData(name, phoneNumber, emailAddress, city, gender, isNotificationEnable, isEmailNotificationEnable);
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
