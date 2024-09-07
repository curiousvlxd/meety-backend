using Domain.Entities.User;
using Telegram.Bot;
namespace UseCases.Features.Users.Get.Common;


public abstract class UserResponse
{
    public string Id { get; set; }
    public string MessengerUserId { get; set; }
    public MessengerType MessengerType { get; set; }

    public static async Task<UserResponse?> FromDomain(User user, ITelegramBotClient client)
    {
        return user.MessengerType switch
        {
            MessengerType.Telegram => await TelegramUserResponse.FromDomain(user, client),
            _ => null
        };
    }
}
