using Domain.Entities.User;
using Domain.Primitives;
using Telegram.Bot;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Users.Get.ByUsername.Dtos;
namespace UseCases.Features.Users.Get.ByUsername;

public sealed class SearchUsersByUsernameQueryHandler(IUserRepository userRepository, ITelegramBotClient client) : IQueryHandler<SearchUsersByUsernameQuery, PagedList<SearchUserResponse?>>
{
    public async Task<PagedList<SearchUserResponse?>> Handle(SearchUsersByUsernameQuery query, CancellationToken cancellationToken)
    {
        var response = await userRepository.GetByUsername(query.Username, query.Pagination, cancellationToken);
        var users = await response.MapAsync(async x => await SearchUserResponse.FromDomain(x, client));
        return users;
    }
}
