using UPT;

var builder = WebApplication
    .CreateBuilder(args)
    .AddOptions()
    .AddAuth()
    .AddDatabase()
    .AddFluentEmail()
    .AddControllersAndSwagger()
    .AddMapsterConfig()
    .AddServices()
    .AddHealthChecks();

var app = builder.Build();
app
    .UseAuth()
    .UseControllers()
    .UseSwaggerAndSwaggerUI()
    .ApplyMigrations()
    .MapHealthChecks();

app.Run();