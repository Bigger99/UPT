using UPT.Domain.Entities;

namespace UPT.Features.Services.City
{
    public interface ICityService
    {
        Task<City> Get(int id);
        Task<List<City>> GetAll();
    }
}