using UPT.Features.Features.FavoritFeatures.Dto;

namespace UPT.Features.Services.Favorit;

public interface IFavoritService
{
    Task<FavoriteDto?> Get(int clientId);
    Task<FavoriteDto> Add(int clientId, int trainerId);
    Task Delete(int clientId, int trainerId);
}