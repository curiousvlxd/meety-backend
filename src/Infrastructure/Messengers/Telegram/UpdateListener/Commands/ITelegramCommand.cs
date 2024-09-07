using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messengers.Telegram.UpdateListener.Commands;

public interface ITelegramCommand
{
    public string Name { get; } 

    public Task Handle(Message message, ITelegramBotClient client)
    {
        return Task.CompletedTask;
    }
}