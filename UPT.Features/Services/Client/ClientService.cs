using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Infrastructure.Middlewars;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.Client;

public class ClientService(UPTDbContext dbContext) : IClientService
{
    public async Task<ClientDto> Get(int clientId)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        return client.Adapt<ClientDto>();
    }

    public async Task<ClientDto> GetByUserId(int userId)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.User.Id == userId) ?? throw new BackendException("Client not found");

        return client.Adapt<ClientDto>();
    }

    public async Task<IEnumerable<ClientWithGoalsDto>> GetFilteredClients(PagedFilterQuery<FilteredClientRequest> pagedFilter)
    {
        var request = pagedFilter.Request;

        var clientsRequest = dbContext.Goals
            .AsNoTracking()
            .Include(x => x.Client)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.City)
            .Where(x => !x.Client.User.IsDeleted)
            .Where(x => x.TrainingProgram == request.GoalTrainingProgram)
            .Where(x => x.TrainerForGoalAchievement == null);

        if (!string.IsNullOrEmpty(pagedFilter.Search))
        {
            var lowerName = pagedFilter.Search.ToLower();
            clientsRequest.Where(x => x.Client.User.Name!.StartsWith(lowerName));
        }

        var goals = await clientsRequest.ToListAsync() ?? throw new BackendException("Client not found");

        var result = new List<ClientWithGoalsDto>();

        foreach (var goal in goals)
        {
            var temp = result.FirstOrDefault(x => x.Id == goal.Client.Id);

            if (temp is not null)
            {
                temp.Goals ??= [];
                temp.Goals.Add(goal.Adapt<SubGoalDto>());
                continue;
            }

            var clientDto = goal.Client.Adapt<ClientWithGoalsDto>();

            if (clientDto is null)
            {
                continue;
            }

            clientDto.Goals ??= [goal.Adapt<SubGoalDto>()];
            result.Add(clientDto);
        }

        return result;
    }

    public async Task<int> Create(int userId, int height, double weight,
        double volumeBreast, double volumeWaist, double volumeAbdomen,
        double volumeButtock, double volumeHip)
    {
        var user = await dbContext.Users
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException($"User with id = {userId}, not found");

        var client = new Domain.Entities.Client(user, height, weight, volumeBreast, volumeWaist, volumeAbdomen, volumeButtock, volumeHip);
        await dbContext.Clients.AddAsync(client);
        await dbContext.SaveChangesAsync();
        return client.Id;
    }

    public async Task<ClientDto> Update(int clientId, int height, double weight,
        double volumeBreast, double volumeWaist, double volumeAbdomen,
        double volumeButtock, double volumeHip)
    {
        var client = await dbContext.Clients
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var clients = new List<Domain.Entities.Client>();

        client.Update(height, weight, volumeBreast, volumeWaist, volumeAbdomen, volumeButtock, volumeHip);
        await dbContext.SaveChangesAsync();
        return client.Adapt<ClientDto>();
    }

    public async Task<ClientDto> SetTrainer(int clientId, int trainerId)
    {
        var client = await dbContext.Clients
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        client.SetTrainer(trainer);
        await dbContext.SaveChangesAsync();
        return client.Adapt<ClientDto>();
    }

    public async Task Delete(int clientId)
    {
        var trainer = await dbContext.Clients
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        trainer.Delete();
        await dbContext.SaveChangesAsync();
    }
}
