using Refit;
using UPT.Features.Features.CityFeatures.Dto;

namespace UPT.Tests.API.Frontend.CityTests.Base;

internal interface ICityProvider
{
    [Get("/api/web/city/get")]
    Task<ApiResponse<CityDto>> Get(int id);

    [Get("/api/web/city/get-all")]
    Task<ApiResponse<List<CityDto>>> GetAll();
}
