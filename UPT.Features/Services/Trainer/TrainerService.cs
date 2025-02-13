using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Middlewars;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.Trainer;

public class TrainerService(UPTDbContext dbContext) : ITrainerService
{
    public async Task<TrainerDto> Get(int id)
    {
        var trainer = await dbContext.Trainers
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Trainer not found");

        var trainerDto = trainer.Adapt<TrainerDto>();
        trainerDto.Rating = await GetTrainerRating(trainer);
        return trainerDto;
    }

    public async Task<TrainerDto> GetByUserId(int userId)
    {
        var trainer = await dbContext.Trainers
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.User.Id == userId) ?? throw new BackendException("Trainer not found");

        var trainerDto = trainer.Adapt<TrainerDto>();
        trainerDto.Rating = await GetTrainerRating(trainer);
        return trainerDto;
    }

    public async Task<IEnumerable<TrainerDto>> GetFilteredTrainers(PagedFilterQuery<FilteredTrainerRequest> pagedFilter)
    {
        var request = pagedFilter.Request;

        var trainersRequest = dbContext.Trainers
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
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
        var result = trainers.Select(x =>
        {
            var trainerDto = x.Adapt<TrainerDto>();
            trainerDto.Rating = GetTrainerRating(x).GetAwaiter().GetResult();
            return trainerDto;
        });
        return result;
    }

    public async Task<int> Create(int userId, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId, string? description)
    {
        var gym = await dbContext.Gyms
            .FirstOrDefaultAsync(x => x.Id == gymId) ?? throw new BackendException($"Gym with id = {gymId} not found");

        var user = await dbContext.Users
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var trainer = new Domain.Entities.Trainer(user, experience, medicGrade, workInjuries, workSportsmens, trainingProgram, gym, description);
        await dbContext.Trainers.AddAsync(trainer);
        await dbContext.SaveChangesAsync();
        return trainer.Id;
    }

    public async Task<TrainerDto> Update(int id, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId, string? description)
    {
        var trainer = await dbContext.Trainers
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Trainer not found");

        var gym = await dbContext.Gyms
            .FirstOrDefaultAsync(x => x.Id == gymId) ?? throw new BackendException($"Gym with id = {gymId} not found");

        trainer.Update(experience, medicGrade, workInjuries, workSportsmens, trainingProgram, gym, description);
        await dbContext.SaveChangesAsync();

        var trainerDto = trainer.Adapt<TrainerDto>();
        trainerDto.Rating = await GetTrainerRating(trainer);
        return trainerDto;
    }

    public async Task<TrainerDto> SetClients(int trainerId, List<int> clientsIds)
    {
        var trainer = await dbContext.Trainers
           .Include(x => x.User)
           .Include(x => x.Gym)
           .Include(x => x.Clients)
           .Where(x => !x.IsDeleted)
           .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var clients = await dbContext.Clients
            .Where(x => clientsIds.Contains(x.Id))
            .ToListAsync();

        trainer.SetClients(clients);
        await dbContext.SaveChangesAsync();

        var trainerDto = trainer.Adapt<TrainerDto>();
        trainerDto.Rating = await GetTrainerRating(trainer);
        return trainerDto;
    }

    public async Task<TrainerDto> DialogCountDecremen(int trainerId)
    {
        var trainer = await dbContext.Trainers
           .Include(x => x.User)
           .Include(x => x.Gym)
           .Include(x => x.Clients)
           .Where(x => !x.IsDeleted)
           .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        if (trainer.DialogCount - 1 <= 0)
        {
            throw new BackendException("The attempts to contact the client have ended");
        }

        trainer.DialogCountDecrement();
        await dbContext.SaveChangesAsync();

        var trainerDto = trainer.Adapt<TrainerDto>();
        trainerDto.Rating = await GetTrainerRating(trainer);
        return trainerDto;
    }

    public async Task<bool> AccessToPublishNews(int trainerId)
    {
        var trainer = await dbContext.Trainers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        return trainer.PurchasedProduct == PurchasedProduct.ProSubscribe || trainer.PurchasedProduct == PurchasedProduct.DeluxeSubscribe;
    }

    public async Task Delete(int id)
    {
        var trainer = await dbContext.Trainers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Trainer not found");

        trainer.Delete();
        await dbContext.SaveChangesAsync();
    }

    private async Task<double> GetTrainerRating(Domain.Entities.Trainer trainer)
    {
        var rating = await dbContext.Feedbacks
            .Where(x => x.Trainer == trainer)
            .ToListAsync();

        var ratingSum = rating.Sum(x => x.Rating);
        return ratingSum / rating.Count;
    }
}
