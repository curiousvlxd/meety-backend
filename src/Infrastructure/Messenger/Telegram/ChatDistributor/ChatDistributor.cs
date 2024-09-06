using Infrastructure.Messenger.Telegram.BotCommandHandler;
using Infrastructure.Messenger.Telegram.TelegramBot;
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
            listener = new TelegramUpdateListener(telegramBot.GetTelegramBotClient());
            _listeners.Add(chatId, listener);
            await listener.GetUpdate(update);
            return;
        }
        
        await listener.GetUpdate(update);
    }
}