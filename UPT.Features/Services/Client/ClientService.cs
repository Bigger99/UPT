using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.ClientFeatures.Dto;
using UPT.Features.Features.ClientFeatures.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Middlewars;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.Client;

public class ClientService(UPTDbContext dbContext) : IClientService
{
    public async Task<ClientDto> Get(int clientId)
    {
        var trainer = await dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        return trainer.Adapt<ClientDto>();
    }

    public async Task<ClientDto> GetByUserId(int userId)
    {
        var trainer = await dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.User.Id == userId) ?? throw new BackendException("Client not found");

        return trainer.Adapt<ClientDto>();
    }

    public async Task<IEnumerable<ClientDto>> GetFilteredClients(PagedFilterQuery<ClientRequest> pagedFilter)
    {
        var request = pagedFilter.Request;

        var clientsRequest = dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .Where(x => x.TrainingProgram == request.TrainingProgram);

        if (!string.IsNullOrEmpty(pagedFilter.Search))
        {
            var lowerName = pagedFilter.Search.ToLower();
            clientsRequest.Where(x => x.User.Name!.StartsWith(lowerName));
        }

        var clients = await clientsRequest.ToListAsync() ?? throw new BackendException("Client not found");
        var result = clients.Select(x => x.Adapt<ClientDto>());
        return result;
    }

    public async Task<int> Create(int userId, TrainingProgram trainingProgram, int height, double weight,
        double volumeBreast, double volumeWaist, double volumeAbdomen,
        double volumeButtock, double volumeHip)
    {
        var user = await dbContext.Users
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException($"User with id = {userId}, not found");

        var client = new Domain.Entities.Client(user, trainingProgram, height, weight, volumeBreast, volumeWaist, volumeAbdomen, volumeButtock, volumeHip);
        await dbContext.Clients.AddAsync(client);
        await dbContext.SaveChangesAsync();
        return client.Id;
    }

    public async Task<ClientDto> Update(int clientId, TrainingProgram trainingProgram, int height, double weight,
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

        client.Update(trainingProgram, height, weight, volumeBreast, volumeWaist, volumeAbdomen, volumeButtock, volumeHip);
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
