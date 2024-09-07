using Infrastructure.Messengers.Telegram.ChatDistributor;
using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Messengers.Telegram.Webhook;

public sealed class HandleWebhookCommandHandler(ITelegramChatDistributor telegramChatDistributor): ICommand
{
    public async Task Handle(HandleWebhookCommand command, CancellationToken cancellationToken)
    {
        await telegramChatDistributor.GetUpdate(command.Update);
    }
}
