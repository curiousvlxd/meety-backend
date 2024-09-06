using Domain.Primitives;
namespace Domain.Entities.User;

public class User : Entity
{
    public required UserId Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
}
