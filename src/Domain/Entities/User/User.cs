using Domain.Primitives;
namespace Domain.Entities.User;

public class User : Entity
{
    public required UserId Id { get; set; }
    public required string Username { get; set; }
    public required MessengerUserId MessengerUserId { get; set; }
    public required ChatId ChatId { get; set; }
    public required MessengerType MessengerType { get; set; }
    public IEnumerable<Invitation.Invitation> ReceivedInvitations { get; } = [];
    public IEnumerable<Invitation.Invitation> SentInvitations { get; } = [];
    public IEnumerable<Meeting.Meeting> Meetings { get; } = [];
    
    public static User Create(string username, MessengerUserId messengerUserId, ChatId chatId, MessengerType messengerType)
    {
        return new User
        {
            Id = new UserId(Ulid.NewUlid()),
            Username = username,
            MessengerUserId = messengerUserId,
            ChatId = chatId,
            MessengerType = messengerType
        };
    }
}