using Api.Abstractions;
using Api.Apis.Meetings.Contracts;
using Api.Extensions;
using Domain.Entities.Meeting;
using Domain.Primitives;
using UseCases.Features.Meetings.Create;
using UseCases.Features.Meetings.Get.ByUser;
namespace Api.Apis.Meetings;

public class CreateMeetingEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Constants.MeetingsApi}/", CreateMeeting)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.MeetingsApi))
            .Produces<PagedList<Meeting>>()
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> CreateMeeting(CreateMeetingContract contract, [AsParameters] MeetingServices services)
    {
        var userId = services.HttpContextAccessor.GetUserId();
        await services.Mediator.Send(new CreateMeetingCommand(contract.Name, userId , contract.Agenda, contract.Date));
        return Results.Ok();
    }
}
