using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.FeedbackFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Feedback;

public class FeedbackService(UPTDbContext dbContext) : IFeedbackService
{
    public async Task<List<FeedbackDto>?> Get(int trainerId)
    {
        var feedbacks = await dbContext.Feedbacks
            .Include(x => x.Creator)
                .ThenInclude(x => x.User)
            .Include(x => x.Trainer)
                .ThenInclude(x => x.User)
            .Where(x => x.Trainer.Id == trainerId)
            .ToListAsync();

        if (feedbacks is null)
        {
            return null;
        }

        return feedbacks.Select(x => x.Adapt<FeedbackDto>()).ToList();
    }

    public async Task<FeedbackDto> Add(int clientId, int trainerId, double rating, string text)
    {
        var client = await dbContext.Clients
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Trainer)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .AsNoTracking()
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Include(x => x.Gym)
            .Include(x => x.Clients)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var existedFeedback = await dbContext.Feedbacks
            .Include(x => x.Creator)
            .Include(x => x.Trainer)
            .FirstOrDefaultAsync(x => x.Creator == client && x.Trainer == trainer);

        if (existedFeedback is not null)
        {
            throw new BackendException("Feedback from current client to this trainer already exists");
        }

        var newFeedback = new Domain.Entities.Feedback(DateTime.UtcNow, rating, text, client, trainer);
        await dbContext.Feedbacks.AddAsync(newFeedback);
        await dbContext.SaveChangesAsync();

        return newFeedback.Adapt<FeedbackDto>();
    }

    public async Task Delete(int clientId, int trainerId)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var feedback = await dbContext.Feedbacks
            .Include(x => x.Creator)
            .Include(x => x.Trainer)
            .FirstOrDefaultAsync(x => x.Creator == client && x.Trainer == trainer);

        if (feedback is null)
        {
            return;
        }

        dbContext.Feedbacks.Remove(feedback);
        await dbContext.SaveChangesAsync();
    }
}
