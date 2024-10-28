using UPT.Features.Features.GymFeatures.Dto;

namespace UPT.Features.Services.Gym;

public interface IGymService
{
    Task<GymDto> Get(int id);
    Task<List<GymDto>> GetAll();
}