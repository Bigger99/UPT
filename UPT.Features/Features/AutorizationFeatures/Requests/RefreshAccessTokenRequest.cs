using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.AutorizationFeatures.Requests;

public class RefreshAccessTokenRequest
{
    [Required]
    public string RefreshToken { get; init; } = default!;
}
