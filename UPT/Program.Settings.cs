using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UPT.Features.Base;
using UPT.Infrastructure;
using UPT.Infrastructure.Interfaces;

namespace UPT;

internal static class Settings
{
    private static Assembly[] _assemblies =
    {
        typeof(Settings).Assembly,
        typeof(BaseController).Assembly
    };

    public static WebApplicationBuilder AddDatabase<T>(this WebApplicationBuilder builder)
    where T : DbContext
    {
        builder.Services.AddDbContext<T>(opt =>
        {
            opt.UseNpgsql(
                builder.Configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly(typeof(T).Assembly.ToString()));
        });

        //builder.Services.AddDbContextAndQueryables<T>(sp => sp.GetRequiredService<T>());
        return builder;
    }

    public static WebApplicationBuilder AddMapsterConfig(this WebApplicationBuilder builder)
    {
        TypeAdapterConfig.GlobalSettings.Scan(
            _assemblies);
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IDto).IsAssignableFrom(type))
                {
                    // get the type handle for the class, invokes static constructor
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
            }
        }
        return builder;
    }

    public static WebApplicationBuilder AddControllersAndSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
        builder.Services.AddCors();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.ToString());
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "API - V1",
                    Version = "v1"
                }
            );
            foreach (var assembly in _assemblies)
            {
                var filePath = Path.Combine($"{assembly.GetName().Name}.xml");
                var path = Path.Combine(AppContext.BaseDirectory, filePath);

                if (File.Exists(path))
                    c.IncludeXmlComments(path);
            }
        });
        return builder;
    }

    public static WebApplication UseSwaggerAndSwaggerUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }

    public static WebApplication ApplyMigrations<T>(this WebApplication app)
        where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<T>();
        context.Database.Migrate();
        //context.SeedData();

        return app;
    }

    public static WebApplication UseControllers(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        return app;
    }

    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        return builder;
    }

    public static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthz");
        return app;
    }
}
