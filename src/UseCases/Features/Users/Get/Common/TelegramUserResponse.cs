using Telegram.Bot;
using Telegram.Bot.Types;
using User = Domain.Entities.User.User;
namespace UseCases.Features.Users.Get.Common;

public class TelegramUserResponse : UserResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? LanguageCode { get; set; }
    public UserProfilePhotos? UserProfilePhotos { get; set; }

    public static async Task<TelegramUserResponse?> FromDomain(User user, ITelegramBotClient client)
    {
        var chatMember = await client.GetChatMemberAsync(user.ChatId.Value, long.Parse(user.MessengerUserId.Value));
        var userPhotos = await client.GetUserProfilePhotosAsync(long.Parse(user.MessengerUserId.Value));
        
        return new TelegramUserResponse
        {
            Id = user.Id.Value.ToString(),
            MessengerUserId = chatMember.User.Id,
            MessengerType = user.MessengerType,
            FirstName = chatMember.User.FirstName,
            LastName = chatMember.User.LastName,
            Username = chatMember.User.Username,
            LanguageCode = chatMember.User.LanguageCode,
            UserProfilePhotos = userPhotos
        };

    }
}
