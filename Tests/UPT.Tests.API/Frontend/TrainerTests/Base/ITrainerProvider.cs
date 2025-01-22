using Refit;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Requests;
using UPT.Infrastructure.Models;

namespace UPT.Tests.API.Frontend.AuthorizeTests.Base;

internal interface ITrainerProvider
{
    [Get("/api/web/trainer/get")]
    Task<ApiResponse<TrainerDto>> Get([Query] int trainerId);

    [Get("/api/web/trainer/get-by-user-id")]
    Task<ApiResponse<TrainerDto>> GetByUserId([Query] int userid);

    [Post("/api/web/trainer/get-filtered")]
    Task<ApiResponse<IEnumerable<TrainerDto>>> GetFiltered([Body] PagedFilterQuery<FilteredTrainerRequest> request);

    [Post("/api/web/trainer/create")]
    Task<ApiResponse<int>> Create([Body] CreateTrainerCommand command);

    [Put("/api/web/trainer/update")]
    Task<ApiResponse<TrainerDto>> Update([Body] UpdateTrainerCommand command);

    [Put("/api/web/trainer/set-clients")]
    Task<ApiResponse<TrainerDto>> SetClients([Body] SetTrainerClientsCommand command);

    [Put("/api/web/trainer/dialog-count-decremen")]
    Task<ApiResponse<TrainerDto>> DialogCountDecremen([Query] int trainerId);

    [Delete("/api/web/trainer/delete")]
    Task<IApiResponse> Delete([Query] int trainerId);
}