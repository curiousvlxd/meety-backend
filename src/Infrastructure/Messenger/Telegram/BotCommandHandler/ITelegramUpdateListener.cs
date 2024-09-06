using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.BotCommandHandler;

public interface ITelegramUpdateListener
{
    Task GetUpdate(Update update);
}
