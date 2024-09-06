using Domain.Primitives;
namespace Domain.Entities.User;

public class User : Entity
{
    public required new UserId Id { get; set; }
    public required string Email { get; set; }
    public MessengerProfile.MessengerProfile? MessengerProfile { get; set; }
    public ICollection<Participation.Participation> Participations { get; } = [];
}
