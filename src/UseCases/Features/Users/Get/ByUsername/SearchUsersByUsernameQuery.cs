using Domain.Primitives;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Users.Get.ByUsername.Dtos;
namespace UseCases.Features.Users.Get.ByUsername;

public sealed record SearchUsersByUsernameQuery(string Username, Pagination Pagination) : IQuery<PagedList<SearchUserResponse>>;
