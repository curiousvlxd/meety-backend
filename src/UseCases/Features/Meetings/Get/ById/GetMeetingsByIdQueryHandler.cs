using Domain.Entities.Meeting;
using UseCases.Abstractions.Messaging;
using UseCases.Features.Meetings.Get.Comon;
namespace UseCases.Features.Meetings.Get.ById;

public class GetMeetingsByIdQueryHandler(IMeetingRepository meetingRepository) : IQueryHandler<GetMeetingByIdQuery, MeetingResponse?>
{
    public async Task<MeetingResponse?> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting =  await meetingRepository.GetByIdAsync(new MeetingId(Ulid.Parse(request.Id)), cancellationToken);
        
        if (meeting is null) return null;
        
        return await MeetingResponse.FromDomain(meeting);
    }
}
