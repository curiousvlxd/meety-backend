using Domain.Entities.User;
using Infrastructure.Authentication.Service;
using Telegram.Bot;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Messengers.Telegram.Webhook.Auth.Dtos;
namespace UseCases.Features.Messengers.Telegram.Webhook.Auth;

public sealed class HandleAuthCommandHandler(
    IJwtService jwtService,
    IUserRepository userRepository) : ICommandHandler<HandleAuthCommand, HandleAuthResponse?>
{
    public async Task<HandleAuthResponse?> Handle(HandleAuthCommand command, CancellationToken cancellationToken)
    {
        var (messengerUserId, username, refresh) = (new MessengerUserId(command.MessengerUserId.ToString()), command.Username, command.Refresh);
        
        var user = await userRepository.GetAsync(messengerUserId, cancellationToken);

        if (user is null)
        {
            if (refresh) return default;
            
            user = User.Create(username, messengerUserId, MessengerType.Telegram);
            await userRepository.CreateAsync(user, cancellationToken);
        }
        
        var token = await jwtService.GenerateJwtTokenAsync(user.Id);
        var refreshToken = await jwtService.GenerateRefreshTokenAsync(user.Id);
        var response = HandleAuthResponse.Create(token, refreshToken);
        return response;
    }
}
