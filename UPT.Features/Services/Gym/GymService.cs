using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.GymFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Gym;

public class GymService(UPTDbContext dbContext) : IGymService
{
    public async Task<List<GymDto>> GetAll()
    {
        var gyms = await dbContext.Gyms
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .ToListAsync();

        return gyms.Select(x => x.Adapt<GymDto>()).ToList();
    }

    public async Task<GymDto> Get(int id)
    {
        var gym = await dbContext.Gyms
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Gym not found");

        return gym.Adapt<GymDto>();
    }
}
