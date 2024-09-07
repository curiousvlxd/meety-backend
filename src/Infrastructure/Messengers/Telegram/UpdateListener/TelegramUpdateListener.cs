using Infrastructure.Messengers.Telegram.UpdateListener.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messengers.Telegram.UpdateListener;

public class TelegramUpdateListener(ITelegramBotClient telegramBotClient) : ITelegramUpdateListener
{
    private readonly List<ITelegramCommand> _commands =
        [new StartCommand(), new HelpCommand()];

    public async Task ProcessUpdate(Update update) 
    {
        var message = update.Message;
        
        if(message?.Text == null)
            return;

        foreach (var command in _commands.Where(command => command.Name == message.Text))
        {
            await command.Handle(message, telegramBotClient);
        }
    }
}
