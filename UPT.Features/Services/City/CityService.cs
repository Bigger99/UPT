using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.City;

public class CityService(UPTDbContext dbContext) : ICityService
{
    public async Task<List<Domain.Entities.City>> GetAll()
    {
        var city = await dbContext.Cities
            .ToListAsync();

        return city;
    }

    public async Task<Domain.Entities.City> Get(int id)
    {
        var gym = await dbContext.Cities
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("City not found");

        return gym;
    }
}
