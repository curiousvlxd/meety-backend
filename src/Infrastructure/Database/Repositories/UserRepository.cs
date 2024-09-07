using Domain.Entities.User;
using Domain.Primitives;
using Infrastructure.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Database.Repositories;

public sealed class UserRepository(IApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        return user;
    }
    
    public async Task<User?> GetAsync(MessengerUserId userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.MessengerUserId == userId, cancellationToken);
        return user;
    }
    
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var entity = await context.Users.AddAsync(user, cancellationToken);
        return entity.Entity;
    }
}
