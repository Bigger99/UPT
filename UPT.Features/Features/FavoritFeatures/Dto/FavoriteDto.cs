using Mapster;
using UPT.Domain.Entities;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.FavoritFeatures.Dto;

public class FavoriteDto : IDto
{
    public int ClientId { get; protected set; } = default!;
    public List<FavoriteTrainerDto> Trainers { get; protected set; } = default!;

    static FavoriteDto()
    {
        TypeAdapterConfig<Favorite, FavoriteDto>
            .NewConfig()
            .Map(dest => dest.ClientId, src => src.Client.Id)
            .Map(dest => dest.Trainers, src => src.Trainers.Select(x => x.Adapt<FavoriteTrainerDto>()));
    }
}
