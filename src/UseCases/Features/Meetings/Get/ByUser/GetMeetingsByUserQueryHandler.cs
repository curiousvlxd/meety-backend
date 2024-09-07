using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Meetings.Get.Comon;
namespace UseCases.Features.Meetings.Get.ByUser;

public class GetMeetingsByUserQueryHandler(IMeetingRepository meetingRepository) : IQueryHandler<GetMeetingsByUserQuery, PagedList<MeetingResponse>>
{
    public async Task<PagedList<MeetingResponse>> Handle(GetMeetingsByUserQuery request, CancellationToken cancellationToken)
    {
        var creatorId = new UserId(Ulid.Parse(request.UserId));
        var pagination = request.Pagination;
        var meetings = await meetingRepository.GetAsync(creatorId, pagination, cancellationToken);
        var response = await meetings.MapAsync(MeetingResponse.FromDomain);
        return response;
    }
}
