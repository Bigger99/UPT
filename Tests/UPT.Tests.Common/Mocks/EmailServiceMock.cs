using FluentEmail.Core;
using UPT.Infrastructure.Email.Service;
using UPT.Infrastructure.Email;

namespace UPT.Common.Mocks;

internal class EmailServiceMock(IFluentEmail fluentEmail) : IEmailService
{
    public async Task Send(EmailMetadata emailMetadata)
    {
        await Task.CompletedTask;
    }
}