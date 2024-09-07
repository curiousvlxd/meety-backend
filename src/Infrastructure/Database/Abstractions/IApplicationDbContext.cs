using Domain.Entities.Invitation;
using Domain.Entities.Meeting;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Infrastructure.Database.Abstractions;

public interface IApplicationDbContext
{   
    DbSet<User> Users { get; set; }
    DbSet<Meeting> Meetings { get; set; }
    DbSet<Invitation> Invitations { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}
