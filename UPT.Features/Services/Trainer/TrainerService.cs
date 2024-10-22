using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Domain.Entities;
using UPT.Features.Features.Trainer.Dto;
using UPT.Features.Features.Trainer.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Middlewars;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.User;

public class TrainerService(UPTDbContext dbContext) : ITrainerService
{
    public async Task<TrainerDto> Get(int id)
    {
        var trainer = await dbContext.Trainers
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Trainer not found");

        return trainer.Adapt<TrainerDto>();
    }

    public async Task<TrainerDto> GetByUserId(int userId)
    {
        var trainer = await dbContext.Trainers
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.User.Id == userId) ?? throw new BackendException("Trainer not found");

        return trainer.Adapt<TrainerDto>();
    }

    public async Task<IEnumerable<TrainerDto>> GetFilteredTrainers(PagedFilterQuery<TrainerRequest> pagedFilter)
    {
        var request = pagedFilter.Request;

        var trainersRequest = dbContext.Trainers
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .Where(x => x.TrainingPrograms.Contains(request.TrainingProgram));

        if (request.GymId.HasValue)
        {
            trainersRequest.Where(x => x.Gym.Id == request.GymId);
        }

        if (!string.IsNullOrEmpty(pagedFilter.Search))
        {
            var lowerName = pagedFilter.Search.ToLower();
            trainersRequest.Where(x => x.User.Name!.StartsWith(lowerName));
        }

        var trainers = await trainersRequest.ToListAsync() ?? throw new BackendException("Trainer not found");
        var result = trainers.Select(x => x.Adapt<TrainerDto>());
        return result;
    }

    public async Task<int> Create(int userId, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId)
    {
        var gym = await dbContext.Gyms
            .FirstOrDefaultAsync(x => x.Id == gymId) ?? throw new BackendException($"Gym with id = {gymId} not found");

        var user = await dbContext.Users
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var trainer = new Trainer(user, experience, medicGrade, workInjuries, workSportsmens, trainingProgram, gym);
        await dbContext.Trainers.AddAsync(trainer);
        await dbContext.SaveChangesAsync();
        return trainer.Id;
    }

    public async Task<TrainerDto> Update(int id, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, List<int> clientsIds, int gymId)
    {
        var trainer = await dbContext.Trainers
            .Include(x => x.User)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.User.Id == id) ?? throw new BackendException("Trainer not found");

        var gym = await dbContext.Gyms
            .FirstOrDefaultAsync(x => x.Id == gymId) ?? throw new BackendException($"Gym with id = {gymId} not found");

        var clients = new List<Client>();

        if (clientsIds.Count != 0)
        {
            clients = await dbContext.Clients
            .Where(x => clientsIds.Contains(x.Id))
            .ToListAsync();
        }

        trainer.Update(experience, medicGrade, workInjuries, workSportsmens, trainingProgram, clients, gym);
        await dbContext.SaveChangesAsync();
        return trainer.Adapt<TrainerDto>();
    }

    public async Task Delete(int id)
    {
        var trainer = await dbContext.Trainers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Trainer not found");

        trainer.Delete();
        await dbContext.SaveChangesAsync();
    }
}
