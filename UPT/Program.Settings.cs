using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UPT.Data;
using UPT.Features.Base;
using UPT.Features.Services.Autorization;
using UPT.Features.Services.User;
using UPT.Infrastructure;
using UPT.Infrastructure.Interfaces;
using UPT.Infrastructure.Jwt;
using UPT.Infrastructure.PasswordHasher;
using UPT.Data.SeedData;

namespace UPT;

internal static class Settings
{
    private static Assembly[] _assemblies =
    {
        typeof(Settings).Assembly,
        typeof(BaseController).Assembly
    };

    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UPTDbContext>(opt =>
        {
            opt.UseNpgsql(
                builder.Configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly(typeof(UPTDbContext).Assembly.ToString()))
            .UseSnakeCaseNamingConvention();
        });

        // dotnet ef migrations add InitialCreate --project "..\UPT.Data\"
        return builder;
    }
    
    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
        return builder;
    }

    public static WebApplicationBuilder AddMapsterConfig(this WebApplicationBuilder builder)
    {
        TypeAdapterConfig.GlobalSettings.Scan(_assemblies);

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IDto).IsAssignableFrom(type))
                {
                    // invokes static constructor
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
            }
        }
        return builder;
    }
    
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<IAutorizationService, AutorizationService>();
        builder.Services.AddScoped<IUserService, UserService>();

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

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement() 
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>() 
                }
            });
        });
        return builder;
    }

    public static WebApplication UseSwaggerAndSwaggerUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<UPTDbContext>();

        if (app.Environment.IsDevelopment())
        {
            context.Database.EnsureDeleted();
        }

        context.Database.Migrate();
        context.SeedData();

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
