namespace Api.Apis.Messengers.Telegram.Contracts;

public record SetWebhookContract(string WebhookUrl, string AuthGuid);
