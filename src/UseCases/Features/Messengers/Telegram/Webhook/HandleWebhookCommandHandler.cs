using Infrastructure.Messenger.Telegram.ChatDistributor;
using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Messengers.Telegram.Webhook;

public sealed class HandleWebhookCommandHandler(IChatDistributor chatDistributor): ICommand
{
    public async Task Handle(HandleWebhookCommand command, CancellationToken cancellationToken)
    {
        await chatDistributor.GetUpdate(command.Update);
    }
}
