using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.Behaviors;
namespace UseCases;

public static class HostBuilderExtension
{
    public static void ConfigureUseCasesLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMediatr<UseCasesAssemblyReference>();
        hostBuilder.Services.AddValidatorsFromAssemblyContaining<UseCasesAssemblyReference>();
    }

    private static void ConfigureMediatr<TAssemblyType>(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(TAssemblyType).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}
