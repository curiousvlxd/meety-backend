using Infrastructure.Messengers.Telegram.Bot;
using MediatR;
using Telegram.Bot;
namespace Api.Apis.Messengers.Telegram;

public class TelegramServices(
    ILogger<TelegramServices> logger,
    IMediator mediator,
    ITelegramBotClient botClient)
{
    public ILogger<TelegramServices> Logger => logger;
    public IMediator Mediator => mediator;
    public ITelegramBotClient BotClient => botClient;
}
