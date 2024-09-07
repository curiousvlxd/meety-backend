namespace Api.Apis.Meetings.Contracts;

public record CreateMeetingContract
{
    public required string Name { get; set; }
    public required string Agenda { get; set; }
    public required DateTime Date { get; set; }
}
