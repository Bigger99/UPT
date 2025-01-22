using Refit;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Features.Features.UserFeatures.Requests;

namespace UPT.Tests.API.Frontend.UserTests.Base;

internal interface IUserProvider
{
    [Get("/api/web/user/get")]
    Task<ApiResponse<UserDto>> Get([Query] int id);

    [Get("/api/web/user/get-by-email")]
    Task<ApiResponse<UserDto>> GetByEmail([Query] GetByEmailQuery query);

    [Put("/api/web/user/update")]
    Task<ApiResponse<UserDto>> Update([Body] UpdateUserCommand command);

    [Put("/api/web/user/email-confirmed")]
    Task<IApiResponse> EmailConfirmed([Body] ConfirmeEmailCommand command);

    [Delete("/api/web/user/delete")]
    Task<IApiResponse> Delete([Query] int id);
}
