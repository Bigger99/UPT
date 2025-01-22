using Refit;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Features.Features.FavoritFeatures.Dto;
using UPT.Features.Features.FavoritFeatures.Requests;

namespace UPT.Tests.API.Frontend.UserTests.Base;

internal interface IFavoritProvider
{
    [Get("/api/web/favorit/get")]
    Task<ApiResponse<FavoriteDto>> Get(int clientId);    
    
    [Delete("/api/web/favorit/delete")]
    Task<IApiResponse> Delete([Body] DeleteFavoritCommand command);

    [Post("/api/web/favorit/add")]
    Task<ApiResponse<List<CityDto>>> Add([Body] AddFavoritCommand command);
}