
using Domain.Entities.Invitation;
using Domain.Entities.Meeting;
using Domain.Entities.User;
using Infrastructure.Database;
using Infrastructure.Database.Abstractions;
using Infrastructure.Database.Options;
using Infrastructure.Database.Repositories;
using Infrastructure.MeetingService;
using Infrastructure.MeetingService.Options;
using Infrastructure.Messengers.Options;
using Infrastructure.Messengers.Telegram.Bot;
using Infrastructure.Messengers.Telegram.ChatDistributor;
using Infrastructure.Messengers.Telegram.Service;
using Infrastructure.Messengers.Telegram.UpdateListener;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using ZoomNet;

namespace Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructureLayer(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.ConfigureMessengers();
        hostBuilder.ConfigureDatabase();
        hostBuilder.ConfigureMessengers();
        hostBuilder.RegisterRepositories();
        hostBuilder.RegisterServices();
        hostBuilder.ConfigureZoom();
    }

    private static void ConfigureMessengers(this IHostApplicationBuilder hostBuilder)
    {       
        hostBuilder.Services.ConfigureOptions<MessengerOptionsSetup>();
        var options = hostBuilder.Services.BuildServiceProvider().GetRequiredService<IOptions<MessengerOptions>>().Value;
        hostBuilder.ConfigureTelegram(options);
    }
    
    private static void ConfigureZoom(this IHostApplicationBuilder hostBuilder)
    {
        var options = hostBuilder.Services.BuildServiceProvider().GetRequiredService<IOptions<MeetingServiceOptions>>().Value;

        hostBuilder.Services.AddSingleton<IZoomClient, ZoomClient>(sp =>
        {   var connectionInfo = new JwtConnectionInfo(options.ZoomApiKey, options.ZoomApiSecret);
            var zoomClient = new ZoomClient(connectionInfo);
            return zoomClient;
        });
        hostBuilder.Services.AddScoped<IMeetingService, MeetingService.MeetingService>();
    }
    
    private static void ConfigureTelegram(this IHostApplicationBuilder hostBuilder, MessengerOptions options)
    {   
        hostBuilder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(sp =>
        {
            var telegramBotClient = new TelegramBotClient(options.TelegramApiKey);
            return telegramBotClient;
        });
        hostBuilder.Services.ConfigureTelegramBot<Microsoft.AspNetCore.Mvc.JsonOptions>(opt => opt.JsonSerializerOptions);

        hostBuilder.Services.AddSingleton<ITelegramBot, TelegramBot>();
        hostBuilder.Services.AddSingleton<ITelegramUpdateListener, TelegramUpdateListener>();
        hostBuilder.Services.AddScoped<ITelegramChatDistributor, TelegramTelegramChatDistributor>();
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
            .AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>())
            .AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
    
    private static void RegisterRepositories(this IHostApplicationBuilder builder)
    {
       builder.Services.AddScoped<IUserRepository, UserRepository>();
       builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
       builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
    }
    
    private static void RegisterServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ITelegramService, TelegramService>();
    }
}
