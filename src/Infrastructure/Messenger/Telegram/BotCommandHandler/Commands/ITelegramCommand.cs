using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.BotCommandHandler.Commands;

public interface ITelegramCommand
{
    public string Name { get; } 

    public Task Handle(Message message, TelegramBotClient client)
    {
        return Task.CompletedTask;
    }
}