using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.MessengerProfile;

public class MessengerProfile: Entity
{
    public required new MessengerProfileId Id { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public UserId UserId { get; set; } 
    public User.User User { get; set; } = null!;
}
