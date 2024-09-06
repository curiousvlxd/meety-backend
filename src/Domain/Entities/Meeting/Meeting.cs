using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Meeting;

public class Meeting : Entity
{
    public required new MeetingId Id { get; set; }
    public UserId CreatorId { get; set; }
    public required string Name { get; set; }
    public required DateTime Scheduled { get; set; }
    public required string Url { get; set; }
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    public ICollection<Invitation.Invitation> Invitations { get; } = [];
}
