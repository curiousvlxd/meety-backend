using Api.Abstractions;
using Api.Extensions;
using Domain.Entities.Meeting;
using Domain.Primitives;
using UseCases.Features.Meetings.Get.ByUser;
namespace Api.Apis.Meetings;

public class GetMeetingsByUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Constants.MeetingApi}/", GetCurrentUser)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.MeetingApi))
            .Produces<PagedList<Meeting>>()
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCurrentUser([AsParameters] Pagination pagination, [AsParameters] MeetingServices services)
    {   
       var userId = services.HttpContextAccessor.GetUserId();
       var query = new GetMeetingsByUserQuery(userId, pagination);
       var response = await services.Mediator.Send(query);
       return Results.Ok(response);
    }
}
