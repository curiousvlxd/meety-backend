using MediatR;
using Telegram.Bot;
namespace Api.Endpoints.Messengers.Telegram;

public class TelegramServices(
    ILogger<TelegramServices> logger,
    IMediator mediator, 
    TelegramBotClient bot)
{
    public ILogger<TelegramServices> Logger => logger;
    public IMediator Mediator => mediator;
    public TelegramBotClient Bot => bot;
}
