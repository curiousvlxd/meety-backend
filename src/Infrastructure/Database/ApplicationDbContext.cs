using Infrastructure.Database.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Infrastructure.Database;

public class AppDbContext(IOptions<DatabaseOptions> options) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(options.Value.Postgres);
    }
}
