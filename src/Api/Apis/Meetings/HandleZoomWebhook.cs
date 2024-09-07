
using Api.Abstractions;
using Api.Extensions;
using UseCases.Features.Meetings.Webhook;
using ZoomNet;
namespace Api.Apis.Meetings;

public class HandleZoomWebhook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Constants.MeetingApi}/zoom/webhook", HandleWebhook)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.MeetingApi))
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }
    
    private static async Task<IResult> HandleWebhook([AsParameters] MeetingServices services)
    {
        // var parser = new WebhookParser();
        // var @event = await parser.ParseEventWebhookAsync(new Stream()).ConfigureAwait(false);
        //
        // await services.Mediator.Send(new HandleZoomWebhookCommand(@event));
        return Results.Ok();
    }
}
