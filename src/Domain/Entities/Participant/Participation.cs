using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Participant;

public sealed class Participation : Entity
{
    public required new ParticipantId Id { get; set; }
    public required DateTime Invited { get; set; }
    public MeetingId MeetingId { get; set; }
    public Meeting.Meeting Meeting { get; set; } = null!;
    public UserId UserId { get; set; }
    public User.User User { get; set; } = null!;
}
