using System.Threading.Tasks.Dataflow;
using Api.Abstractions;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace Api.Apis.Meetings;

// public class HandleZoomWebhook : IEndpoint
// {
//     public void MapEndpoint(IEndpointRouteBuilder app)
//     {
//         app.MapPost($"{Constants.MeetingsApi}/zoom/webhook", HandleWebhook)
//             .MapToApiVersion(1)
//             .WithTags(nameof(Constants.MeetingsApi))
//             .ProduceProblems(StatusCodes.Status400BadRequest,
//                 StatusCodes.Status401Unauthorized,
//                 StatusCodes.Status404NotFound);
//     }
//     
//     private static async Task<IResult> HandleWebhook([FromBody] JoinBlock<> contract, [AsParameters] MeetingServices services)
//     {
//         if(contract.AuthGuid != Constants.ZoomWebhookAuthGuid)
//             return Results.Unauthorized();
//         
//         await services.Mediator.Send(new HandleZoomWebhookCommand(contract));
//         return Results.Ok();
//     }
// }
