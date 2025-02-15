using Microsoft.AspNetCore.Mvc;
using Refit;
using UPT.Features.Features.GoalFeatures.Dto;
using UPT.Features.Features.GoalFeatures.Requests;

namespace UPT.Tests.API.Frontend.GoalTests.Base;

internal interface IGoalProvider
{
    [Get("/api/web/goal/get")]
    Task<ApiResponse<GoalDto?>> Get([Query] int clientId);

    [Get("/api/web/goal/get-all-client-goals")]
    Task<ApiResponse<List<GoalDto>?>> GetAllClientGoals([Query] int clientId);

    [Post("/api/web/goal/create")]
    Task<ApiResponse<GoalDto>> Create([Body] CreateGoalCommand command);

    [Put("/api/web/goal/update")]
    Task<ApiResponse<GoalDto>> Update([Body] UpdateGoalCommand command);

    [Put("/api/web/goal/set-trainer")]
    Task<ApiResponse<GoalDto>> SetTrainer([Body] SetGoalTrainerCommand command);

    [Delete("/api/web/goal/delete")]
    Task<IApiResponse> Delete([FromQuery] int goalId);
}