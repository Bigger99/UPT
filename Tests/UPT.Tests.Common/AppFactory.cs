using FluentEmail.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using UPT.Common.Mocks;
using UPT.Infrastructure.Email.Service;

namespace UPT.Common;

public class AppFactory(PgFixture pgFixture) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:default", pgFixture.ConnectionString);

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = TestAuthHandler.TestScheme;
                o.DefaultChallengeScheme = TestAuthHandler.TestScheme;
            }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.TestScheme, _ => { });

            services.AddScoped<IFluentEmail, FluentEmailMock> ();
            services.AddScoped<IEmailService, EmailServiceMock> ();
        });

        base.ConfigureWebHost(builder);
    }
}
