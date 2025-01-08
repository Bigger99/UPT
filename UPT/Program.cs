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