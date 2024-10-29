using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.NewsFeatures.Dto;

public class NewsDto : IDto
{
    public int Id { get; protected set; } = default!;
    public string? Name { get; protected set; } = default!;

    public DateTime CreationDate { get; protected set; } = default!;

    public DateTime? EditDate { get; protected set; } = default!;

    public string Text { get; protected set; } = default!;

    public UserDto User { get; protected set; } = default!;

    static NewsDto()
    {
        TypeAdapterConfig<News, NewsDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
