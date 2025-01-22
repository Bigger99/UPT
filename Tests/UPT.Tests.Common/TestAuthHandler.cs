using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UPT.Common;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string TestScheme = nameof(TestScheme);
    public const string TestUser = "59813870-DE19-47B0-9A85-B5E139EDA4E7";


    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var isCreateUserEndpoint = Context.Request.Path.Value == "/api/web/user/create-user";

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, isCreateUserEndpoint ? Guid.NewGuid().ToString() : TestUser) };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
