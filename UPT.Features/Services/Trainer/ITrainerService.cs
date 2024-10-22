using UPT.Features.Features.Trainer.Dto;
using UPT.Features.Features.Trainer.Requests;
using UPT.Infrastructure.Enums;
using UPT.Infrastructure.Models;

namespace UPT.Features.Services.User;

public interface ITrainerService
{
    Task Delete(int id);
    Task<TrainerDto> Get(int id);
    Task<TrainerDto> GetByUserId(int userId);
    Task<IEnumerable<TrainerDto>> GetFilteredTrainers(PagedFilterQuery<TrainerRequest> pagedFilter);
    Task<TrainerDto> Update(int id, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, List<int> clientsIds, int gymId);
    Task<int> Create(int userId, int experience, bool medicGrade, bool workInjuries, bool workSportsmens, List<TrainingProgram> trainingProgram, int gymId);
}