using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NewsFeatures.Requests;

public class AddNewsCommand
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = default!;

    [Required]
    public int UserId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = default!;
}
