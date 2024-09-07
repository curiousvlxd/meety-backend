using Domain.Entities.Invitation;
using UseCases.Abstractions.Messaging;
using ZoomNet;
namespace UseCases.Features.Meetings.Webhook;

public sealed class HandleZoomWebhookCommandHandler(IInvitationRepository invitationRepository, IZoomClient zoomClient): ICommandHandler<HandleZoomWebhookCommand>
{
    public async Task Handle(HandleZoomWebhookCommand command, CancellationToken cancellationToken)
    {
    }
}