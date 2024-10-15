using UPT;
using UPT.Data;

var builder = WebApplication
    .CreateBuilder(args)
    .AddDatabase<UPTDbContext>()
    .AddControllersAndSwagger()
    .AddMapsterConfig()
    .AddHealthChecks();

var app = builder.Build();
app
    .UseControllers()
    .UseSwaggerAndSwaggerUI()
    .ApplyMigrations<UPTDbContext>()
    .MapHealthChecks();

app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
