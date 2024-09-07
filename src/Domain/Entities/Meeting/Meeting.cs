using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Meeting;

public class Meeting : Entity
{
    public required MeetingId Id { get; set; }
    public UserId CreatorId { get; set; }
    public User.User? Creator { get; set; }
    public required long ZoomMeetingId { get; set; }
    public required string Agenda { get; set; }
    public required string Name { get; set; }
    public required DateTime Scheduled { get; set; }
    public required string Url { get; set; }
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    public IEnumerable<Invitation.Invitation> Invitations { get; } = [];
}
