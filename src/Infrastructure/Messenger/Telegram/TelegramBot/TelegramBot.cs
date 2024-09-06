using Microsoft.Extensions.Options;
using Telegram.Bot;
namespace Infrastructure.Messenger.Telegram.TelegramBot;

public class TelegramBot(IOptions<MessengerOptions.MessengerOptions> messengerOptions): ITelegramBot
{
    private TelegramBotClient? Client { get; set; }
        
    public TelegramBotClient GetTelegramBotClient()
    {
        Client = new TelegramBotClient(messengerOptions.Value.TelegramApiKey);
        return Client;
    }
}
