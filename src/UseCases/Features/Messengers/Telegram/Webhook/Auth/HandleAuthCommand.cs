using UseCases.Abstractions.Messaging;
using UseCases.Features.Messengers.Telegram.Webhook.Auth.Dtos;
namespace UseCases.Features.Messengers.Telegram.Webhook.Auth;

public sealed record HandleAuthCommand(long ChatId, long MessengerUserId, string? Username, bool Refresh = false) : ICommand<HandleAuthResponse?>;
