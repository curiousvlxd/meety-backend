using Domain.Entities.User;
using Telegram.Bot;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Users.Get.Common;
namespace UseCases.Features.Users.Get.ById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository, ITelegramBotClient client) : IQueryHandler<GetUserByIdQuery, UserResponse?>
{
    public async Task<UserResponse?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {   
        var userId = new UserId(Ulid.Parse(query.Id));
        var user = await userRepository.GetAsync(userId, cancellationToken);
        
        if (user is null) return null;
        
        var response = await UserResponse.FromDomain(user, client);
        return response;
    }
}
