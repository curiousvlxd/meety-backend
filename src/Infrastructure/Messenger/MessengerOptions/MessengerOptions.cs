namespace Infrastructure.Messenger.MessengerOptions;

public sealed record MessengerOptions
{
    public required string TelegramApiKey { get; set; }
}
