using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Messengers.Telegram.Webhook;

public sealed class HandleWebhookCommandHandler: ICommand
{
    public async Task Handle(HandleWebhookCommand command, CancellationToken cancellationToken)
    {
        
    }
}
