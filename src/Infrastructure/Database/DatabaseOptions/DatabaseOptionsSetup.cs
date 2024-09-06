using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Database.DatabaseOptions;

public class DatabaseOptionsSetup(IConfiguration configuration): IConfigureOptions<DatabaseOptions>
{
    private const string SectionName = "Database";

    public void Configure(DatabaseOptions options) => configuration.GetSection(SectionName).Bind(options);
}
