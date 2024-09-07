using System.Reflection;
using FluentValidation;
using Infrastructure.Logging.Pipeline;
using MediatR;
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
        // hostBuilder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        // hostBuilder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        // hostBuilder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void ConfigureMediatr<TAssemblyType>(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(TAssemblyType).Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}
