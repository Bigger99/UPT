using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.FeedbackFeatures.Requests;

public class DeleteFeedbackCommand
{
    [Required] public int ClientId { get; set; } = default!;
    [Required] public int TrainerId { get; set; } = default!;
}
