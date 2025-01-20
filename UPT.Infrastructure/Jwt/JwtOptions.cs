namespace UPT.Infrastructure.Jwt;

public class JwtOptions
{
    public string SecretKey { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public int ExpiredHours { get; init; }
    public int RefreshTokenExpirationDays { get; init; }
}