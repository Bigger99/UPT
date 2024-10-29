using UPT.Features.Features.TrainerFeatures.Dto;
using UPT.Features.Features.TrainerFeatures.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.Trainer;

public interface ITrainerService
{
    Task<int> Create(int userId, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId, string? description);
    Task Delete(int id);
    Task<TrainerDto> Get(int id);
    Task<TrainerDto> GetByUserId(int userId);
    Task<IEnumerable<TrainerDto>> GetFilteredTrainers(PagedFilterQuery<TrainerRequest> pagedFilter);
    Task<TrainerDto> SetClients(int trainerId, List<int> clientsIds);
    Task<TrainerDto> DialogCountDecremen(int trainerId);
    Task<TrainerDto> Update(int id, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId, string? description);
}