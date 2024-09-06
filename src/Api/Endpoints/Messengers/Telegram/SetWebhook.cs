// using Api.Abstractions;
// using Api.Extensions;
// using Telegram.Bot;
// using UseCases.Features.Messengers.Telegram.Webhook;
// namespace Api.Endpoints.Messengers.Telegram;
//
// /// <summary>
// /// Represents the endpoint to set webhook url.
// /// </summary>
// public class SetWebhookEndpoint: IEndpoint
// {
//     public void MapEndpoint(IEndpointRouteBuilder app)
//     {
//         app.MapPost($"{Constants.TelegramApi}/telegram/webhook", SetWebhook)
//             .MapToApiVersion(1)
//             .WithTags(nameof(Constants.TelegramApi))
//             .ProduceProblems(StatusCodes.Status400BadRequest,
//                 StatusCodes.Status401Unauthorized,
//                 StatusCodes.Status404NotFound);
//     }
//
//     private static async Task<IResult> SetWebhook(string webhookUrl, string authGuid, [AsParameters] TelegramServices services)
//     {
//         await services.Bot.SetWebhookAsync(webhookUrl);
//         return Results.Ok();
//     }
// }
