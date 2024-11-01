using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.AutorizationFeatures.Requests;

public class EditPasswordCommand
{
    [Required]
    public string EmailAddress { get; init; } = default!;

    [Required]
    public string OldPassword { get; init; } = default!;

    [Required]
    public string NewPassword { get; init; } = default!;
}
