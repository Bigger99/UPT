using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Gym;

public class GymService(UPTDbContext dbContext) : IGymService
{
    public async Task<List<Domain.Entities.Gym>> GetAll()
    {
        var gyms = await dbContext.Gyms
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .ToListAsync();

        return gyms;
    }

    public async Task<Domain.Entities.Gym> Get(int id)
    {
        var gym = await dbContext.Gyms
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Gym not found");

        return gym;
    }
}
