using Microsoft.AspNetCore.Mvc;
using Refit;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Infrastructure.Models;

namespace UPT.Tests.API.Frontend.AuthorizeTests.Base;

internal interface IClientProvider
{
    [Get("/api/web/client/get")]
    Task<ApiResponse<ClientDto>> Get([Query] int clientId);

    [Get("/api/web/client/get-by-user-id")]
    Task<ApiResponse<ClientDto>> GetByUserId([Query] int userid);

    [Post("/api/web/client/get-filtered")]
    Task<ApiResponse<IEnumerable<ClientWithGoalsDto>>> GetFiltered([Body] PagedFilterQuery<FilteredClientRequest> request);

    [Post("/api/web/client/create")]
    Task<ApiResponse<int>> Create([Body] CreateClientCommand command);

    [Put("/api/web/client/update")]
    Task<ApiResponse<ClientDto>> Update([Body] UpdateClientCommand command);

    [Put("/api/web/client/set-trainer")]
    Task<ApiResponse<ClientDto>> SetTrainer([Body] SetClientTrainerCommand command);

    [Delete("/api/web/client/delete")]
    Task<IApiResponse> Delete([FromQuery] int clientId);
}