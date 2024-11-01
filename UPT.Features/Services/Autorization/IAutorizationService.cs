namespace UPT.Features.Services.Autorization;

public interface IAutorizationService
{
    Task EditPassword(string emailAddress, string oldPassword, string newPassword);
    Task<string> Login(string email, string password);
    Task Register(string email, string password);
    Task RestorePassword(string emailAddress);
}