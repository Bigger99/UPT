namespace UPT.Infrastructure.Jwt;

public interface IJwtProvider
{
    string GenerateToken(int userId);
}