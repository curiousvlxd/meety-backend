#region

using Api.Abstractions;
using Api.Apis.Messengers.Telegram.Contracts;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

#endregion

namespace Api.Apis.Messengers.Telegram;

/// <summary>
/// Represents the endpoint for getting social media.
/// </summary>
public class SetWebhookEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Constants.TelegramApi}/telegram/webhook", SetHandleWebhook)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.TelegramApi))
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> SetHandleWebhook([FromBody] SetWebhookContract contract, [AsParameters] TelegramServices services)
    {
        const string guid = "e3119897-472b-4f54-a47c-fa2424dd2bc1";
        
        if(contract.AuthGuid != guid)
            return Results.Unauthorized();
        
        await services.BotClient.SetWebhookAsync(contract.WebhookUrl);
        return Results.Ok();
    }
}
