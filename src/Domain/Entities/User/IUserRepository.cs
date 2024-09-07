namespace Domain.Entities.User;

public interface IUserRepository
{
    Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<User?> GetAsync(MessengerUserId userId, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
}
