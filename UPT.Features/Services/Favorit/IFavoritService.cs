using UPT.Features.Features.FavoritFeatures.Dto;

namespace UPT.Features.Services.Favorit;

public interface IFavoritService
{
    Task<FavoriteDto> Add(int clientId, int trainerId);
    Task Delete(int clientId, int trainerId);
    Task<List<FavoriteDto>> Get(int clientId);
}