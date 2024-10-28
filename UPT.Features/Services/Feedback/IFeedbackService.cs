using UPT.Features.Features.FeedbackFeatures.Dto;

namespace UPT.Features.Services.Feedback;

public interface IFeedbackService
{
    Task<FeedbackDto> Add(int clientId, int trainerId, double rating, string text);
    Task Delete(int clientId, int trainerId);
    Task<List<FeedbackDto>?> Get(int trainerId);
}