using UseCases.Abstractions.Messaging;
using UseCases.Features.Users.Get.Common;
namespace UseCases.Features.Users.Get.ById;

public sealed record GetUserByIdQuery(string Id) : IQuery<UserResponse?>;