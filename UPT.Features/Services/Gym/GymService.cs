﻿using Mapster;
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
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .ToListAsync();

        return gyms.Select(x => x.Adapt<GymDto>()).ToList();
    }

    public async Task<GymDto> Get(int id)
    {
        var gym = await dbContext.Gyms
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.City)
            .Include(x => x.Trainers)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new BackendException("Gym not found");

        return gym.Adapt<GymDto>();
    }

    public async Task<List<GymDtoWithTrainer>> GetAllWithTrainers()
    {
        var gyms = await dbContext.Gyms
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.City)
            .Include(x => x.Trainers.Where(x => !x.IsDeleted))
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.City)
            .ToListAsync();

        return gyms.Select(x => x.Adapt<GymDtoWithTrainer>()).ToList();
    }
}
