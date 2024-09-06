
using Infrastructure.Database;
using Infrastructure.Database.Options;
using Infrastructure.Messenger.MessengerOptions;
using Infrastructure.Messenger.Telegram.ChatDistributor;
using Infrastructure.Messenger.Telegram.TelegramBot;
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
        hostBuilder.ConfigureTelegramBot();
    }

    private static void ConfigureMessenger(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<MessengerOptionsSetup>();
    }
    
    private static void ConfigureChatDistributor(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddScoped<IChatDistributor, ChatDistributor>();
    }
    
    private static void ConfigureTelegramBot(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddSingleton<ITelegramBot, TelegramBot>();
    }

    private static void ConfigureDatabase(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<DatabaseOptionsSetup>();
        hostBuilder.Services.AddDbContext<AppDbContext>(options =>
        {
            var databaseOptions = hostBuilder.Services.BuildServiceProvider().GetRequiredService<IOptions<DatabaseOptions>>();
            options
                .UseNpgsql(databaseOptions.Value.Postgres)
                .UseSnakeCaseNamingConvention();
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
    }
}
