using Domain.Entities.Invitation;
using Domain.Entities.Meeting;
using Domain.Entities.User;
using Infrastructure.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext, IUnitOfWork
{

    public DbSet<User> Users { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => await base.SaveChangesAsync(cancellationToken);
}
