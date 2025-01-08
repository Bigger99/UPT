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
using UPT.Infrastructure.Middlewars;
using UPT.Features.Services.Gym;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UPT.Filters;
using UPT.Features.Services.Trainer;
using UPT.Features.Services.Client;
using UPT.Features.Services.City;
using UPT.Features.Services.Favorit;
using UPT.Features.Services.Feedback;
using UPT.Features.Services.Payment;
using UPT.Features.Services.Notification;
using UPT.Features.Services.News;
using UPT.Infrastructure.Email;
using UPT.Infrastructure.Email.Service;
using UPT.Features.Services.Goal;

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

    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        var preciseConfig = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() 
            ?? throw new InvalidOperationException($"{nameof(JwtOptions)} is null");

        builder.Services.AddAuthorization();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = preciseConfig.Issuer,
                    ValidAudience = preciseConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(preciseConfig.SecretKey))
                };
            });

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
        builder.Services.AddScoped<IGymService, GymService>();
        builder.Services.AddScoped<ITrainerService, TrainerService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<ICityService, CityService>();
        builder.Services.AddScoped<IFavoritService, FavoritService>();
        builder.Services.AddScoped<IFeedbackService, FeedbackService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<INewsService, NewsService>();
        builder.Services.AddScoped<IGoalService, GoalService>();

        return builder;
    }

    public static WebApplicationBuilder AddFluentEmail(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmailService, EmailService>();

        var emailOptions = builder.Configuration.GetSection(nameof(EmailOptions)).Get<EmailOptions>()
            ?? throw new InvalidOperationException($"{nameof(EmailOptions)} is null");

        builder.Services.AddFluentEmail(emailOptions.FromEmail)
            .AddSmtpSender(emailOptions.Host, emailOptions.Post, emailOptions.UserName, emailOptions.Password);

        return builder;
    }

    public static WebApplicationBuilder AddControllersAndSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        builder.Services.AddCors(o => o.AddPolicy("UPT_Cors", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            options.SwaggerDoc("v1",
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
                    options.IncludeXmlComments(path);
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement() 
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

            options.OperationFilter<AuthorizeLockChecker>();
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

    public static WebApplication UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static WebApplication UseControllers(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.UseCors("UPT_Cors");

        return app;
    }

    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        return builder;
    }    
    
    public static WebApplicationBuilder AddSignalR(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();
        return builder;
    }

    public static WebApplication MapHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthz");
        return app;
    }
}
