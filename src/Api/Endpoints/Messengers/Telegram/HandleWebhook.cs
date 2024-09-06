#region

using Api.Abstractions;
using Api.Extensions;
using UseCases.Features.Messengers.Telegram.Webhook;

#endregion

namespace Api.Endpoints.Messengers.Telegram;

/// <summary>
/// Represents the endpoint for getting social media.
/// </summary>
public class HandleWebhookEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Constants.TelegramApi}/webhook", HandleWebhook)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.TelegramApi))
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleWebhook([AsParameters] TelegramServices services)
    {
       await services.Mediator.Send(new HandleWebhookCommand());
       return Results.Ok();
    }
}
