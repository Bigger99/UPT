using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.FavoritFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Favorit;

public class FavoritService(UPTDbContext dbContext) : IFavoritService
{
    public async Task<List<FavoriteDto>> Get(int clientId)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var favorite = await dbContext.Favorits
            .Include(x => x.Trainer)
            .Where(x => x.Client == client)
            .ToListAsync();

        return favorite.Select(x => x.Adapt<FavoriteDto>()).ToList();
    }

    public async Task<FavoriteDto> Add(int clientId, int trainerId)
    {
        var client = await dbContext.Clients
            .FirstOrDefaultAsync(x => x.Id == clientId) ?? throw new BackendException("Client not found");

        var trainer = await dbContext.Trainers
            .FirstOrDefaultAsync(x => x.Id == trainerId) ?? throw new BackendException("Trainer not found");

        var favorite = await dbContext.Favorits
            .Include(x => x.Trainer)
            .FirstOrDefaultAsync(x => x.Client == client && x.Trainer == trainer);

        if (favorite is not null )
        {
             throw new BackendException("Trainer exists in the favorite list");
        }

        var newFavorite = new Domain.Entities.Favorite(client, trainer);
        await dbContext.Favorits.AddAsync(newFavorite);
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
            .Include(x => x.Trainer)
            .FirstOrDefaultAsync(x => x.Client == client && x.Trainer == trainer);

        if (favorite is not null)
        {
            dbContext.Favorits.Remove(favorite);
            await dbContext.SaveChangesAsync();
        }
    }
}
