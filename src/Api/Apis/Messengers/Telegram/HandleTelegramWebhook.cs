#region

using Api.Abstractions;
using Api.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Api.Apis.Messengers.Telegram;
    
public class HandleTelegramWebhookEndpoint : IEndpoint
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

        await services.TelegramUpdateListener.ProcessUpdate(update);

        return Results.Ok();
    }
}
