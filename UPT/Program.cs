using System.Diagnostics.CodeAnalysis;
using UPT;
using UPT.SignalR.Hubs;

var builder = WebApplication
    .CreateBuilder(args)
    .AddOptions()
    .AddAuth()
    .AddDatabase()
    .AddFluentEmail()
    .AddControllersAndSwagger()
    .AddMapsterConfig()
    .AddServices()
    .AddHealthChecks()
    .AddSignalR();

var app = builder.Build();
app
    .UseAuth()
    .UseControllers()
    .UseSwaggerAndSwaggerUI()
    .ApplyMigrations()
    .MapHealthChecks()
    .MapHub<ChatHub>("/chat");

app.Run();

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public partial class Program
{
    [ExcludeFromCodeCoverage]
    protected Program() { }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member