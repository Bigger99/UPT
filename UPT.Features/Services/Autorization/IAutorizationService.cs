namespace UPT.Features.Services.Autorization;

public interface IAutorizationService
{
    Task<string> Login(string email, string password);
    Task Register(string email, string password);
}