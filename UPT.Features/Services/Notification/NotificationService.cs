using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.NotificationFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Notification;

public class NotificationService(UPTDbContext dbContext) : INotificationService
{
    public async Task<List<NotificationDto>?> GetCkecked(int userId)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var notifications = await dbContext.Notifications
            .Include(x => x.User)
            .Where(x => !x.IsChecked && x.User.Id == userId)
            .ToListAsync();

        if (notifications is null)
        {
            return null;
        }

        return notifications.Select(x => x.Adapt<NotificationDto>()).ToList();
    }

    public async Task<List<NotificationDto>?> GetUnCkecked(int userId)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var notifications = await dbContext.Notifications
            .Include(x => x.User)
            .Where(x => x.IsChecked && x.User.Id == userId)
            .ToListAsync();

        if (notifications is null)
        {
            return null;
        }

        return notifications.Select(x => x.Adapt<NotificationDto>()).ToList();
    }

    public async Task<NotificationDto> Create(string name, string text, int userId)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var newNotification = new Domain.Entities.Notification(name, DateTime.UtcNow, text, user);
        await dbContext.Notifications.AddAsync(newNotification);
        await dbContext.SaveChangesAsync();

        return newNotification.Adapt<NotificationDto>();
    }

    public async Task SetChecked(int notificationId)
    {
        var notification = await dbContext.Notifications
            .FirstOrDefaultAsync(x => x.Id == notificationId);

        if (notification is null)
        {
            return;
        }

        notification.SetChecked();
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(int notificationId)
    {
        var notification = await dbContext.Notifications
            .FirstOrDefaultAsync(x => x.Id == notificationId);

        if (notification is null)
        {
            return;
        }

        dbContext.Notifications.Remove(notification);
        await dbContext.SaveChangesAsync();
    }
}
