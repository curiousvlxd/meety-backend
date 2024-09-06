using Infrastructure.Logging;
using Serilog;
using Serilog.Events;
namespace Api.Configurations;

public static class LoggingConfiguration
{
    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel
                .Override("Microsoft.AspNetCore.Hosting.Diagnostics",
                    LogEventLevel.Error)
                .MinimumLevel
                .Override("Microsoft.Hosting.Lifetime",
                    LogEventLevel.Information)
                .WriteTo.Console();
        });
    }

    public static void AddRequestLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestBodyLoggingMiddleware>();
        app.UseSerilogRequestLogging();
    }
}
