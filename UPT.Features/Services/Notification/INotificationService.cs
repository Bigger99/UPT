using UPT.Features.Features.NotificationFeatures.Dto;

namespace UPT.Features.Services.Notification;

public interface INotificationService
{
    Task<NotificationDto> Add(string name, string text, int userId);
    Task Delete(int notificationId);
    Task<List<NotificationDto>?> GetCkecked(int userId);
    Task<List<NotificationDto>?> GetUnCkecked(int userId);
    Task SetChecked(int notificationId);
}