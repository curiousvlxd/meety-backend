
using Infrastructure.Messenger.MessengerOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class HostBuilderExtension
{
    public static void ConfigureInfrastructureLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMessenger();
    }

    private static void ConfigureMessenger(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<MessengerOptionsSetup>();
    }
}
