using Refit;
using UPT.Features.Features.NotificationFeatures.Dto;
using UPT.Features.Features.NotificationFeatures.Requests;

namespace UPT.Tests.API.Frontend.NotificationTests.Base;

internal interface INotificationProvider
{
    [Get("/api/web/notification/get-ckecked")]
    Task<ApiResponse<List<NotificationDto>?>> GetCkecked([Query] int userId);

    [Get("/api/web/notification/get-un-ckecked")]
    Task<ApiResponse<List<NotificationDto>?>> GetUnCkecked([Query] int userId);

    [Post("/api/web/notification/create")]
    Task<ApiResponse<NotificationDto?>> Create([Body] CreateNotificationCommand command);

    [Put("/api/web/notification/set-checked")]
    Task<IApiResponse> SetChecked([Query] int notificationId);

    [Delete("/api/web/notification/delete")]
    Task<IApiResponse> Delete([Query] int notificationId);
}
