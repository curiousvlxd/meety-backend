using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.Abstractions;
namespace UseCases;

public static class HostBuilderExtension
{
    public static void ConfigureUseCaseLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMediatr();
    }

    private static void ConfigureMediatr(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ICommand).Assembly));
    }
}
