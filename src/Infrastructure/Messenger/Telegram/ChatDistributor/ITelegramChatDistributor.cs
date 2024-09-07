using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.ChatDistributor;

public interface IChatDistributor
{
    Task GetUpdate(Update update);
}
