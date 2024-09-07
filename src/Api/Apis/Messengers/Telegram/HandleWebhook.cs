#region

using Api.Abstractions;
using Api.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UseCases.Features.Messengers.Telegram.Webhook;
using UseCases.Features.Messengers.Telegram.Webhook.Auth;
using UseCases.Features.Messengers.Telegram.Webhook.Auth.Dtos;

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

    private static async Task<IResult> HandleWebhook(Update update, [AsParameters] TelegramServices services)
    {
        if (update is not { Type: UpdateType.Message, Message.Type: MessageType.Text }) return Results.Ok();
        
        var message = update.Message;
        var chatId = message.Chat.Id;
        var userId = message.From?.Id;
            
        if (userId is null) return Results.BadRequest("User not found.");
            
        var userName = message.From?.Username;
            
        switch (message.Text)
        {
            case "/auth":
            {
                var response = await services.Mediator.Send(new HandleAuthCommand(chatId, userId.Value, userName));
                return response is null ? Results.Unauthorized() : Results.Ok(response);
            }
            case "/refresh-token":
            {
                var response = await services.Mediator.Send(new HandleAuthCommand(chatId, userId.Value, userName, true));
                return response is null ? Results.Unauthorized() : Results.Ok(response);
            }
        }

        await services.TelegramUpdateListener.ProcessUpdate(update);

        return Results.Ok();
    }
}
