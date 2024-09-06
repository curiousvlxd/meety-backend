using Infrastructure.Messenger.Telegram.ChatDistributor;
using Infrastructure.Messenger.Telegram.TelegramBot;
using MediatR;
using Telegram.Bot;
namespace Api.Apis.Messengers.Telegram;

public class TelegramServices(
    ILogger<TelegramServices> logger,
    IMediator mediator,
    ITelegramBot bot,
    IChatDistributor chatDistributor)
{
    public ILogger<TelegramServices> Logger => logger;
    public IMediator Mediator => mediator;
    public ITelegramBot Bot => bot;
    public IChatDistributor ChatDistributor => chatDistributor;
}
