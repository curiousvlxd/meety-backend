namespace Infrastructure.Messengers.Options;

public sealed record MessengerOptions
{
    public required string TelegramApiKey { get; set; }
}
