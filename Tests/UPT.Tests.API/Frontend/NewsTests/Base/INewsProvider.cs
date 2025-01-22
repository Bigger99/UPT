using Refit;
using UPT.Features.Features.NewsFeatures.Dto;
using UPT.Features.Features.NewsFeatures.Requests;

namespace UPT.Tests.API.Frontend.UserTests.Base;

internal interface INewsProvider
{
    [Get("/api/web/news/get")]
    Task<ApiResponse<NewsDto?>> Get([Query] int userId);

    [Get("/api/web/news/get-all")]
    Task<ApiResponse<List<NewsDto>?>> GetAll();

    [Delete("/api/web/news/delete")]
    Task<IApiResponse> Delete([Query] int notificationId);

    [Put("/api/web/news/update")]
    Task<ApiResponse<NewsDto>> Update([Body] UpdateNewsCommand command);

    [Post("/api/web/news/create")]
    Task<ApiResponse<NewsDto>> Create([Body] AddNewsCommand command);
}