using Domain.Exceptions;
using Infrastructure.Authentication;
namespace Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(c => c.Type == UserClaims.Sub);
        return userIdClaim?.Value ?? throw new UnauthorizedException("User ID not found.");
    }
}
