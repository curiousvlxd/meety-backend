using Infrastructure.Messenger.Telegram.BotCommandHandler.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace Infrastructure.Messenger.Telegram.BotCommandHandler;

public class TelegramUpdateListener(TelegramBotClient telegramBotClient) : ITelegramUpdateListener
{
    private readonly List<ITelegramCommand> _commands =
        [new StartCommand(), new HelpCommand()];

    public async Task GetUpdate(Update update) 
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
