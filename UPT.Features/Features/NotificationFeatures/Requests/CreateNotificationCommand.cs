using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NotificationFeatures.Requests;

public class CreateNotificationCommand
{
    [MaxLength(255)]
    public string Name { get; set; } = default!;

    public int UserId { get; set; } = default!;

    [MaxLength(255)]
    public string Text { get; set; } = default!;
}
