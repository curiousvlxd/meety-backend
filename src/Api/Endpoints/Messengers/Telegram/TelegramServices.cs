using MediatR;
namespace Api.Endpoints.Messengers.Telegram;

public class TelegramServices(
    ILogger<TelegramServices> logger,
    IMediator mediator)
{
    public ILogger<TelegramServices> Logger => logger;
    public IMediator Mediator => mediator;
}
