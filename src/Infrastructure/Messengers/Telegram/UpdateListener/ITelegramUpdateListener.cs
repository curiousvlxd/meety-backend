using Telegram.Bot.Types;
namespace Infrastructure.Messengers.Telegram.UpdateListener;

public interface ITelegramUpdateListener
{
    Task ProcessUpdate(Update update);
}
