using Infrastructure.Messengers.Telegram.UpdateListener;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messengers.Telegram.ChatDistributor;

public class TelegramTelegramChatDistributor(ITelegramBotClient client): ITelegramChatDistributor
{
    private readonly Dictionary<long, ITelegramUpdateListener> _listeners = new();

    public async Task GetUpdate(Update update)
    {
        if (update.Message is null)
            return;
        
        var chatId = update.Message.Chat.Id;
        var listener = _listeners.GetValueOrDefault(chatId);
        
        if (listener is null)
        {
            listener = new TelegramUpdateListener(client);
            _listeners.Add(chatId, listener);
            await listener.ProcessUpdate(update);
            return;
        }
        
        await listener.ProcessUpdate(update);
    }
}