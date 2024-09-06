using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Invitation;

public sealed class Invitation : Entity
{
    public required new InvitationId Id { get; set; }
    public required DateTime Visited { get; set; }
    public MeetingId MeetingId { get; set; }
    public Meeting.Meeting? Meeting { get; set; }
    public UserId UserId { get; set; }
    public User.User? User { get; set; }
}