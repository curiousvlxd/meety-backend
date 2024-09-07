using Api.Abstractions;
using Api.Extensions;
using Domain.Primitives;
using UseCases.Features.Users.Get.ByUsername;
using UseCases.Features.Users.Get.Common;
namespace Api.Apis.Users;

public class SearchUsersByUsernameEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Constants.UserApi}/search/username/{{username}}", GetUsersEndpoint)
            .MapToApiVersion(1)
            .WithTags(nameof(Constants.UserApi))
            .Produces<PagedList<UserResponse>>()
            .ProduceProblems(StatusCodes.Status400BadRequest,
                StatusCodes.Status401Unauthorized,
                StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetUsersEndpoint(string username, [AsParameters] Pagination pagination, [AsParameters] UserServices services)
    {   
        var userId = services.HttpContextAccessor.GetUserId();
        var query = new SearchUsersByUsernameQuery(userId, pagination);
        var response = await services.Mediator.Send(query);
        return Results.Ok(response);
    }
}
