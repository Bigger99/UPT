using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NewsFeatures.Requests;

public class UpdateNewsCommand
{
    [Required]
    [MaxLength(255)]
    public int NewsId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = default!;

    [Required]
    public int UserId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = default!;
}
