using Refit;
using UPT.Features.Features.FeedbackFeatures.Dto;
using UPT.Features.Features.FeedbackFeatures.Requests;

namespace UPT.Tests.API.Frontend.FeedbackTests.Base;

internal interface IFeedbackProvider
{
    [Get("/api/web/feedback/get")]
    Task<ApiResponse<List<FeedbackDto>?>> Get([Query] int trainerId);

    [Delete("/api/web/feedback/delete")]
    Task<IApiResponse> Delete([Body] DeleteFeedbackCommand command);

    [Post("/api/web/feedback/add")]
    Task<ApiResponse<FeedbackDto>> Add([Body] AddFeedbackCommand command);
}