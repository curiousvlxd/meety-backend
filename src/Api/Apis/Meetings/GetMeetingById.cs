using Api.Abstractions;
using Api.Extensions;
using Domain.Entities.Meeting;
using Domain.Primitives;
using UseCases.Features.Meetings.Get.ById;
using UseCases.Features.Meetings.Get.ByUser;
namespace Api.Apis.Meetings;

public class GetMeetingByIdEndpoint: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Constants.MeetingApi}/{{id}}", GetMeetingById)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.MeetingApi))
            .Produces<Meeting>()
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetMeetingById(string id, [AsParameters] MeetingServices services)
    {   
        var response = await services.Mediator.Send(new GetMeetingByIdQuery(id));
        return response is not null ? Results.Ok(response) : Results.NotFound();
    }
}
