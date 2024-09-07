using Telegram.Bot.Types;
using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Messengers.Telegram.Webhook;

public sealed record HandleWebhookCommand(Update Update): ICommand;