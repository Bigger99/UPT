using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.AutorizationFeatures.Requests;

public class RefreshAccessTokenRequest
{
    [Required]
    public string AccessToken { get; init; } = default!;
}
