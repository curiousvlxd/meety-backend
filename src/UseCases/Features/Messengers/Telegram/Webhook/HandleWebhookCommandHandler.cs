using UseCases.Abstractions.Messaging;
namespace UseCases.Webhook;

public class HandleWebhookCommandHandler: ICommand
{
    public async Task Handle(HandleWebhookCommand referenceCommand, CancellationToken cancellationToken)
    {
        Console.WriteLine("HandleWebhookCommandHandler");
    }
}
