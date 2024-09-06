using UseCases.Abstractions;
namespace UseCases.Webhook;

public class HandleWebhookCommandHandler: ICommand
{
    public async Task Handle(HandleWebhookCommand referenceCommand, CancellationToken cancellationToken)
    {
        Console.WriteLine("HandleWebhookCommandHandler");
    }
}
