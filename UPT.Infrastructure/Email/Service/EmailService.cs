using FluentEmail.Core;

namespace UPT.Infrastructure.Email.Service;

public class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    public async Task Send(EmailMetadata emailMetadata)
    {
        await fluentEmail.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .Body(emailMetadata.Body)
            .SendAsync();
    }
}