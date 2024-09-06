using Infrastructure.Messenger.Telegram.TelegramBot;
using Infrastructure.Messenger.Telegram.TelegramUpdateListener;
using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.ChatDistributor;

public class ChatDistributor(ITelegramBot telegramBot): IChatDistributor
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
            listener = new TelegramUpdateListener.TelegramUpdateListener(telegramBot.GetTelegramBotClient());
            _listeners.Add(chatId, listener);
            await listener.ProcessUpdate(update);
            return;
        }
        
        await listener.ProcessUpdate(update);
    }
}