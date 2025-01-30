namespace UPT.Infrastructure.Jwt;

public interface IJwtProvider
{
    void DeleteUser(int userId);
    TokensModel GenerateTokens(int userId);
    string RefreshAccessToken(string refreshToken);
}