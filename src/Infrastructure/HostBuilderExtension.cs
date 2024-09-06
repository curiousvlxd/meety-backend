
using Infrastructure.Telegram;
using Infrastructure.Telegram.TelegramOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class HostBuilderExtension
{
    public static void ConfigureInfrastructureLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureTelegram();
    }

    private static void ConfigureTelegram(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.ConfigureOptions<TelegramOptionsSetup>();
    }
}
