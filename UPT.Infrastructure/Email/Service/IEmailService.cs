namespace UPT.Infrastructure.Email.Service;

public interface IEmailService
{
    Task Send(EmailMetadata emailMetadata);
}