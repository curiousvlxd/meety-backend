using Domain.Entities.User;
using Telegram.Bot;
namespace UseCases.Features.Users.Get.ByUsername.Dtos;

public abstract record SearchUserResponse
{
    public string Id { get; set; }
    
    public static async Task<SearchUserResponse?> FromDomain(User user, ITelegramBotClient client)
    {
        return user.MessengerType switch
        {
            MessengerType.Telegram => await SearchTelegramUserResponse.FromDomain(user, client),
            _ => null
        };
    }
}
