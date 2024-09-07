using Telegram.Bot;
using Telegram.Bot.Types;
using User = Domain.Entities.User.User;
namespace UseCases.Features.Users.Get.ByUsername.Dtos;

public record SearchTelegramUserResponse : SearchUserResponse
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LanguageCode { get; set; }
    public UserProfilePhotos? UserProfilePhotos { get; set; }

    public static async Task<SearchTelegramUserResponse?> FromDomain(User user, ITelegramBotClient client)
    {
        
        var chatMember = user.ChatId?.Value is null ? default : await client.GetChatMemberAsync(user.ChatId.Value.Value, long.Parse(user.MessengerUserId.Value));
        var userPhotos = await client.GetUserProfilePhotosAsync(long.Parse(user.MessengerUserId.Value));
        
        return new SearchTelegramUserResponse
        {
            Id = user.Id.Value.ToString(),
            FirstName = chatMember?.User.FirstName,
            LastName = chatMember?.User.LastName,
            Username = chatMember?.User.Username,
            LanguageCode = chatMember?.User.LanguageCode,
            UserProfilePhotos = userPhotos
        };

    }
}
