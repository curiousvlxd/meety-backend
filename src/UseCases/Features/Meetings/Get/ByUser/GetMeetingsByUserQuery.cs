using Domain.Primitives;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Meetings.Get.Comon;
namespace UseCases.Features.Meetings.Get.ByUser;

public sealed record GetMeetingsByUserQuery(string UserId, Pagination Pagination) : IQuery<PagedList<MeetingResponse>>;