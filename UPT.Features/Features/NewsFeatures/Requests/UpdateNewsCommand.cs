using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NewsFeatures.Requests;

public class UpdateNewsCommand
{
    [Required]
    public int NewsId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = default!;

    [Required]
    public int UserId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = default!;

    public string? Image { get; set; } = default!;
}
