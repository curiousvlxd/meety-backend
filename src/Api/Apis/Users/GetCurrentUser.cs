using Api.Abstractions;
using Api.Apis.Messengers.Telegram;
using Api.Extensions;
using UseCases.Features.Messengers.Telegram.Webhook;
namespace Api.Apis.Users;

public class GetCurrentUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Constants.UserApi}/current", GetCurrentUser)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.UserApi))
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCurrentUser([AsParameters] UserServices services)
    {   
        // await services.Mediator.Send(new HandleWebhookCommand());
        return Results.Ok();
    }
}
