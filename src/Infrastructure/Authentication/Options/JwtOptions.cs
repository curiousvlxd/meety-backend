namespace Infrastructure.Authentication.Options;

public sealed record JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Secret { get; init; }
}
