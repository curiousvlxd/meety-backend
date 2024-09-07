namespace UseCases.Features.Messengers.Telegram.Webhook.Auth.Dtos;

public sealed record HandleAuthResponse
{
    public required string JwtToken { get; init; }
    public required string RefreshToken { get; init; }
    
    public static HandleAuthResponse Create(string jwtToken, string refreshToken)
    {
        return new HandleAuthResponse
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken
        };
    }
}
