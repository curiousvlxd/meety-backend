using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Participant;

public sealed class Participant : Entity
{
    public required ParticipantId Id { get; set; }
    public required UserId UserId { get; set; }
    public required MeetingId MeetingId { get; set; }
}
