using Domain.Primitives;
namespace Domain.Entities.User;

public class User : Entity
{
    public required UserId Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public required string MessengerUserId { get; set; }
    public string? ImageUrl { get; set; }
    public required MessengerType MessengerType { get; set; }
    public IEnumerable<Invitation.Invitation> Invitations { get; } = [];
}
