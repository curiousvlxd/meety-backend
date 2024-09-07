#region

using Api.Abstractions;
using Api.Apis.Messengers.Telegram.Contracts;
using Api.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UseCases.Features.Messengers.Telegram.Webhook.Auth;

#endregion

namespace Api.Apis.Messengers.Telegram;

public class HandleTelegramAuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Constants.TelegramApi}/auth", HandleAuth)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.TelegramApi))
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleAuth(AuthContract auth, [AsParameters] TelegramServices services)
    {
       var response = await services.Mediator.Send(new HandleAuthCommand(auth.Id, auth.Username));
       
       return response is null ? Results.Unauthorized() : Results.Ok(response);
    }
}
