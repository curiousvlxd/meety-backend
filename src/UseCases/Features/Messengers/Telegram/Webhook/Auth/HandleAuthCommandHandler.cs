using Domain.Entities.User;
using Infrastructure.Authentication.Service;
using Telegram.Bot;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Messengers.Telegram.Webhook.Auth.Dtos;
namespace UseCases.Features.Messengers.Telegram.Webhook.Auth;

public sealed class HandleAuthCommandHandler(
    IJwtService jwtService,
    IUserRepository userRepository,
    ITelegramBotClient client) : ICommandHandler<HandleAuthCommand, HandleAuthResponse?>
{
    public async Task<HandleAuthResponse?> Handle(HandleAuthCommand command, CancellationToken cancellationToken)
    {
        var (chatId, messengerUserId, username, refresh) = (new ChatId(command.ChatId.ToString()), new MessengerUserId(command.MessengerUserId.ToString()), command.Username, command.Refresh);
        await client.SendTextMessageAsync(chatId.Value, "Please wait...", cancellationToken: cancellationToken);
        
        if (string.IsNullOrWhiteSpace(username))
        {
            await client.SendTextMessageAsync(chatId.Value, "Your username is private or unset. Please set or make it public for using the Meety.", cancellationToken: cancellationToken);
            return default;
        }
        
        var user = await userRepository.GetAsync(messengerUserId, cancellationToken);

        if (user is null)
        {
            if (refresh) return default;
            
            user = User.Create(username, messengerUserId, chatId, MessengerType.Telegram);
            await userRepository.CreateAsync(user, cancellationToken);
        }
        
        var token = await jwtService.GenerateJwtTokenAsync(user.Id);
        var refreshToken = await jwtService.GenerateRefreshTokenAsync(user.Id);
        await client.SendTextMessageAsync(chatId.Value, "You have successfully authenticated.", cancellationToken: cancellationToken);
        var response = HandleAuthResponse.Create(token, refreshToken);
        return response;
    }
}
