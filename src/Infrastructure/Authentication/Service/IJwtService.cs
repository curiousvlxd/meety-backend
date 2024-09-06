namespace Infrastructure.Authentication.Service;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(string messengerUserId, bool isRefreshToken = false);
}
