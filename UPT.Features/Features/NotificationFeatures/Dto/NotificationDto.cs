using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.NotificationFeatures.Dto;

public class NotificationDto : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;

    public DateTime CreationDate { get; set; } = default!;

    [MaxLength(255)]
    public string Text { get; set; } = default!;

    public UserDto User { get; set; } = default!;
    public bool IsChecked { get; set; }

    static NotificationDto()
    {
        TypeAdapterConfig<Notification, NotificationDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
