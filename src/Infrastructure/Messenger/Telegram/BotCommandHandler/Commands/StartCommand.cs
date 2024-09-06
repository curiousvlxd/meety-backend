using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.BotCommandHandler.Commands;

public class StartCommand : ITelegramCommand
{
    public string Name => "/start";

    public async Task Handle(Message message, TelegramBotClient client)
    {
        var chatId = message.Chat.Id;
        await client.SendTextMessageAsync(chatId, "Hello, welcome to Meety telegram bot!");
    }
}