using UPT.Infrastructure.Jwt;

namespace UPT.Features.Services.Autorization;

public interface IAutorizationService
{
    Task EditPassword(string emailAddress, string oldPassword, string newPassword);
    Task<TokensModel> Login(string email, string password);
    Task<string> RefreshAccessToken(string refreshToken);
    Task Register(string email, string password);
    Task RestorePassword(string emailAddress);
}