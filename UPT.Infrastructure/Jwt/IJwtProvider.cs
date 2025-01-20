namespace UPT.Infrastructure.Jwt;

public interface IJwtProvider
{
    TokensModel GenerateTokens(int userId);
    string RefreshAccessToken(string refreshToken);
}