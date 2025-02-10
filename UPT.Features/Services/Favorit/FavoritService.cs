using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.FavoritFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Favorit;

public class FavoritService(UPTDbContext dbContext) : IFavoritService
{
    public async Task<FavoriteDto?> Get(int clientId)
    {
        var client = await dbContext.Clients
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var favorite = await dbContext.Favorits
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.Trainers)
                .ThenInclude(x => x.Gym)
            .Include(x => x.Client)  
            .FirstOrDefaultAsync(x => x.Client == client);

        if (favorite is null)
        {
            return null;
        }

        return favorite.Adapt<FavoriteDto>();
    }

    public async Task<FavoriteDto> Add(int clientId, int trainerId)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var favorite = await dbContext.Favorits
            .Include(x => x.Trainers)
            .FirstOrDefaultAsync(x => x.Client == client);

        if (favorite is not null)
        {
            var existsTrainer = favorite.Trainers.Contains(trainer);

            if (existsTrainer)
            {
                throw new BackendException("Trainer exists in the favorite list");
            }

            favorite.Trainers.Add(trainer);
        }
        else
        {
            var newFavorite = new Domain.Entities.Favorite(client, [trainer]);
            await dbContext.SaveChangesAsync();
        }
        
        await dbContext.SaveChangesAsync();

        return favorite.Adapt<FavoriteDto>();
    }

    public async Task Delete(int clientId, int trainerId)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var favorite = await dbContext.Favorits
            .Include(x => x.Trainers)
            .FirstOrDefaultAsync(x => x.Client == client);

        if (favorite is null)
        {
            return;
        }

        if (favorite.Trainers.Contains(trainer))
        {
            dbContext.Favorits.Remove(favorite);
            await dbContext.SaveChangesAsync();

        }
    }
}
