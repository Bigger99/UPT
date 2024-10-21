namespace UPT.Features.Services.Gym;

public interface IGymService
{
    Task<Domain.Entities.Gym> Get(int id);
    Task<List<Domain.Entities.Gym>> GetAll();
}