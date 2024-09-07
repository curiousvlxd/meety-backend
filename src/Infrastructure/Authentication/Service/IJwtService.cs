using Domain.Entities.User;
namespace Infrastructure.Authentication.Service;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(UserId userId);
    Task<string> GenerateRefreshTokenAsync(UserId userId);
}
