using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.CityFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.City;

public class CityService(UPTDbContext dbContext) : ICityService
{
    public async Task<List<CityDto>> GetAll()
    {
        var cities = await dbContext.Cities
            .AsNoTracking()
            .ToListAsync();

        return cities.Select(x => x.Adapt<CityDto>()).ToList();
    }

    public async Task<CityDto> Get(int id)
    {
        var city = await dbContext.Cities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("City not found");

        return city.Adapt<CityDto>();
    }
}
