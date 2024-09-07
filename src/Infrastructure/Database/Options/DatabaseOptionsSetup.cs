using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Database.Options;

public class DatabaseOptionsSetup(IConfiguration configuration): IConfigureOptions<DatabaseOptions>
{
    private const string SectionName = "Postgres";

    public void Configure(DatabaseOptions options)
    {
        var postgres = configuration.GetConnectionString(SectionName);
        
        if (string.IsNullOrWhiteSpace(postgres)) throw new InvalidOperationException($"Connection string for {SectionName} is missing.");
        
        options.Postgres = postgres;
    }
}
