using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Messengers.Options;

public class MessengerOptionsSetup(IConfiguration configuration) : IConfigureOptions<MessengerOptions>
{
    private const string SectionName = "Messengers";

    public void Configure(MessengerOptions options) => configuration.GetSection(SectionName).Bind(options);
}
