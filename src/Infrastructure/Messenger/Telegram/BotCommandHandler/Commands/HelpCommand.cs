using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.BotCommandHandler.Commands;

public class HelpCommand: ITelegramCommand
{
    public string Name => "/help";

    public async Task Handle(Message message, TelegramBotClient client)
    {
        var chatId = message.Chat.Id;
        await client.SendTextMessageAsync(chatId, "This is help options: ...");
    }
}
