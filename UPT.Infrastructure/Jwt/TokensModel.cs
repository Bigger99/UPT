namespace UPT.Infrastructure.Jwt;

public class TokensModel
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
