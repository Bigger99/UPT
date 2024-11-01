using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.AutorizationFeatures.Requests;

public class RestorePasswordCommand
{
    [Required]
    public string EmailAddress { get; init; } = default!;
}
