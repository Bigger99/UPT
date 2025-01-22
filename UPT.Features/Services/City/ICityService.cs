using UPT.Features.Features.CityFeatures.Dto;

namespace UPT.Features.Services.City;

public interface ICityService
{
    Task<CityDto> Get(int id);
    Task<List<CityDto>> GetAll();
}