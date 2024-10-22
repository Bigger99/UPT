using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.Client;

public interface IClientService
{
    Task<int> Create(int userId, TrainingProgram trainingProgram, int height, double weight, double volumeBreast, double volumeWaist, double volumeAbdomen, double volumeButtock, double volumeHip);
    Task Delete(int clientId);
    Task<ClientDto> Get(int clientId);
    Task<ClientDto> GetByUserId(int userId);
    Task<IEnumerable<ClientDto>> GetFilteredClients(PagedFilterQuery<ClientRequest> pagedFilter);
    Task<ClientDto> SetTrainer(int clientId, int trainerId);
    Task<ClientDto> Update(int clientId, TrainingProgram trainingProgram, int height, double weight, double volumeBreast, double volumeWaist, double volumeAbdomen, double volumeButtock, double volumeHip);
}