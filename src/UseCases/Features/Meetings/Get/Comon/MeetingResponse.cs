using Domain.Entities.Meeting;
namespace UseCases.Features.Meetings.Get.Comon;

public sealed record MeetingResponse
{
    public string Id { get; init; }
    public string CreatorId { get; init; }
    public string Name { get; init; }
    public required DateTime Scheduled { get; set; }
    public string Url { get; init; }
    public MeetingStatus Status { get; init; }

    public static async Task<MeetingResponse> FromDomain(Meeting meeting)
    {
        var response = new MeetingResponse
        {
            Id = meeting.Id.ToString(),
            CreatorId = meeting.CreatorId.ToString(),
            Name = meeting.Name,
            Scheduled = meeting.Scheduled,
            Url = meeting.Url,
            Status = meeting.Status
        };
        
        return await Task.FromResult(response);
    }
}
