
using Infrastructure.Database;
using Infrastructure.Database.DatabaseOptions;
using Infrastructure.Messenger.MessengerOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructureLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMessenger();
        hostBuilder.ConfigureDatabase();
    }

    private static void ConfigureMessenger(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<MessengerOptionsSetup>();
    }

    private static void ConfigureDatabase(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<DatabaseOptionsSetup>();
        hostBuilder.Services.AddDbContext<AppDbContext>();
    }
}
