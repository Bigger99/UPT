using Refit;
using UPT.Features.Features.GymFeatures.Dto;

namespace UPT.Tests.API.Frontend.UserTests.Base;

internal interface IGymProvider
{
    [Get("/api/web/gym/get")]
    Task<ApiResponse<GymDto>> Get(int id);

    [Get("/api/web/gym/get-all")]
    Task<ApiResponse<List<GymDto>>> GetAll();
}
