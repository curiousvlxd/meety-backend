using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Messenger.MessengerOptions;

public class MessengerOptionsSetup(IConfiguration configuration) : IConfigureOptions<MessengerOptions>
{
    private const string SectionName = "Messenger";

    public void Configure(MessengerOptions options) => configuration.GetSection(SectionName).Bind(options);
}
