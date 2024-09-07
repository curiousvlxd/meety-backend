using Api.Abstractions;
using Api.Extensions;
using UseCases.Features.Users.Get.ById;
using UseCases.Features.Users.Get.Common;
namespace Api.Apis.Users;

public class GetCurrentUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Constants.UserApi}/current", GetCurrentUser)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.UserApi))
            .Produces<UserResponse>()
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCurrentUser([AsParameters] UserServices services)
    {   
       var userId = services.HttpContextAccessor.GetUserId();
       var query = new GetUserByIdQuery(userId);
       var user = await services.Mediator.Send(query);
       
       return user is null ? Results.NotFound() : Results.Ok(user);
    }
}
