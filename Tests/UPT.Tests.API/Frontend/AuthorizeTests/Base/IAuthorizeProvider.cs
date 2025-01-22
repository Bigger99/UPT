using Refit;
using UPT.Features.Features.AutorizationFeatures.Requests;
using UPT.Infrastructure.Jwt;

namespace UPT.Tests.API.Frontend.AuthorizeTests.Base;

internal interface IAuthorizeProvider
{
    [Post("/api/web/autorization/register")]
    Task<IApiResponse> Register([Body] RegisterCommand command);

    [Post("/api/web/autorization/restore-password")]
    Task<IApiResponse> RestorePassword([Body] RestorePasswordCommand command);

    [Post("/api/web/autorization/edit-password")]
    Task<IApiResponse> EditPassword([Body] EditPasswordCommand command);

    [Post("/api/web/autorization/login")]
    Task<ApiResponse<TokensModel>> Login([Body] LoginRequest command);

    [Post("/api/web/autorization/refresh-access-token")]
    Task<ApiResponse<string>> RefreshAccessToken([Body] RefreshAccessTokenRequest command);
}