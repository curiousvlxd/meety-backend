using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.TelegramUpdateListener;

public interface ITelegramUpdateListener
{
    Task ProcessUpdate(Update update);
}
