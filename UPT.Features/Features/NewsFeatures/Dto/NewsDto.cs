using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.NewsFeatures.Dto;

public class NewsDto : IDto
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; } = default!;

    public DateTime CreationDate { get; set; } = default!;

    public DateTime? EditDate { get; set; } = default!;

    public string Text { get; set; } = default!;

    public UserDto User { get; set; } = default!;

    static NewsDto()
    {
        TypeAdapterConfig<News, NewsDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
