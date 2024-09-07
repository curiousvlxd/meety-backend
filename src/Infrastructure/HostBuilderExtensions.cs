
using Domain.Entities.Invitation;
using Domain.Entities.User;
using Infrastructure.Database;
using Infrastructure.Database.Abstractions;
using Infrastructure.Database.Options;
using Infrastructure.Database.Repositories;
using Infrastructure.Messenger.MessengerOptions;
using Infrastructure.Messenger.Telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructureLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMessenger();
        hostBuilder.ConfigureDatabase();
        hostBuilder.RegisterRepositories();
        hostBuilder.RegisterServices();
    }

    private static void ConfigureMessenger(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<MessengerOptionsSetup>();
    }

    private static void ConfigureDatabase(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<DatabaseOptionsSetup>();
        hostBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var databaseOptions = hostBuilder.Services.BuildServiceProvider().GetRequiredService<IOptions<DatabaseOptions>>();
                options
                    .UseNpgsql(databaseOptions.Value.Postgres)
                    .UseSnakeCaseNamingConvention();
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            })
            .AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
    
    private static void RegisterRepositories(this IHostApplicationBuilder builder)
    {
       builder.Services.AddScoped<IUserRepository, UserRepository>();
       builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
    }
    
    private static void RegisterServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ITelegramService, TelegramService>();
    }
}
