using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Meeting;

public class Meeting : Entity
{
    public required MeetingId Id { get; set; }
    public UserId CreatorId { get; set; }
    
}
