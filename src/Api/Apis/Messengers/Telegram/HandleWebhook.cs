#region

using Api.Abstractions;
using Api.Extensions;
using Infrastructure.Messenger.Telegram.BotCommandHandler;
using Infrastructure.Messenger.Telegram.ChatDistributor;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using UseCases.Features.Messengers.Telegram.Webhook;

#endregion

namespace Api.Apis.Messengers.Telegram;

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

    private static async Task<IResult> HandleWebhook([FromBody] HandleWebhookCommand command, [AsParameters] TelegramServices services)
    { 
       await services.Mediator.Send(command);
       return Results.Ok();
    }
}
