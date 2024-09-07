using Telegram.Bot.Types;
namespace Infrastructure.Messengers.Telegram.ChatDistributor;

public interface ITelegramChatDistributor
{
    Task GetUpdate(Update update);
}
