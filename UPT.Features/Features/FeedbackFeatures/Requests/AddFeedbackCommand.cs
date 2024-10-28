using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.FeedbackFeatures.Requests;

public class AddFeedbackCommand : DeleteFeedbackCommand
{
    [Range(0.0, 5.0)]
    public double Rating { get; set; } = default!;

    [MaxLength(255)]
    public string Text { get; set; } = default!;

}
