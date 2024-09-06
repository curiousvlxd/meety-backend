using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Infrastructure.Telegram.TelegramOptions;

public class TelegramOptionsSetup(IConfiguration configuration): IConfigureOptions<TelegramOptions>
{
    private const string SectionName = "Telegram";

    public void Configure(TelegramOptions options) => configuration.GetSection(SectionName).Bind(options);
}
