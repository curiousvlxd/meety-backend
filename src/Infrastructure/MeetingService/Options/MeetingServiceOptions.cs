namespace Infrastructure.MeetingService.Options;

public sealed record MeetingServiceOptions
{
    public required string ZoomApiKey { get; set; }
    public required string ZoomApiSecret { get; set; }
}
