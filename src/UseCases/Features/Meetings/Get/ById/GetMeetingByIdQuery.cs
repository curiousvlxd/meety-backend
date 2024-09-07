using Domain.Primitives;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Meetings.Get.Comon;
namespace UseCases.Features.Meetings.Get.ById;

public sealed record GetMeetingByIdQuery(string Id) : IQuery<MeetingResponse?>;