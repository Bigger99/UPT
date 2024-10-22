using UPT.Domain.Entities;

namespace UPT.Features.Services.City
{
    public interface ICityService
    {
        Task<Domain.Entities.City> Get(int id);
        Task<List<Domain.Entities.City>> GetAll();
    }
}