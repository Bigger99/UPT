using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.AutorizationFeatures.Requests;

public class RegisterCommand
{
    [Required]
    public string EmailAddress { get; init; } = default!;

    [Required]
    public string PasswordHash { get; init; } = default!;
}
