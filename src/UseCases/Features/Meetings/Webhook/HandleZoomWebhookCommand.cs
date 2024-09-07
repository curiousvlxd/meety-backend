using UseCases.Abstractions.Messaging;
using ZoomNet.Models.Webhooks;
namespace UseCases.Features.Meetings.Webhook;

public record HandleZoomWebhookCommand(Event ZoomEvent): ICommand;