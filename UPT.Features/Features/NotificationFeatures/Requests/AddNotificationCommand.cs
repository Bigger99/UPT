using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NotificationFeatures.Requests;

public class AddNotificationCommand
{
    [MaxLength(255)]
    public string Name { get; protected set; } = default!;

    public int UserId { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;
}
