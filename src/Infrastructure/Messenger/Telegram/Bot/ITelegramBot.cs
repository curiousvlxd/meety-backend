using Telegram.Bot;
namespace Infrastructure.Messenger.Telegram.TelegramBot;

public interface ITelegramBot
{ 
    TelegramBotClient GetTelegramBotClient();
}
