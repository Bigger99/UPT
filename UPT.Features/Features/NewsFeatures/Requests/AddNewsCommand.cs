using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.NewsFeatures.Requests;

public class AddNewsCommand
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = default!;

    [Required]
    public int UserId { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = default!;
    public byte[]? Image { get; set; } = default!;
}
