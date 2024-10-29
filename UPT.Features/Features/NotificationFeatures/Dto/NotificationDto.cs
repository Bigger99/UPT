using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.NotificationFeatures.Dto;

public class NotificationDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;

    public DateTime CreationDate { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;

    public UserDto User { get; protected set; } = default!;
    public bool IsChecked { get; protected set; }

    static NotificationDto()
    {
        TypeAdapterConfig<Notification, NotificationDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
