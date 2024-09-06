using Domain.Primitives;
namespace Domain.Entities.User;

public class User : Entity
{
    public required new UserId Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public required string MessengerUserId { get; set; }
    public required string ImageUrl { get; set; }
    public IEnumerable<Invitation.Invitation> Invitations { get; } = [];
}
