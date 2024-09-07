using Domain.Entities.Meeting;
using Domain.Entities.User;
using Infrastructure.MeetingService;
using UseCases.Abstractions.Messaging;
namespace UseCases.Features.Meetings.Create;

public class CreateMeetingCommandHandler(IMeetingRepository meetingRepository, IMeetingService meetingService) : ICommandHandler<CreateMeetingCommand>
{
    public async Task Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var creatorId = new UserId(Ulid.Parse(request.UserId));

        var meeting = await meetingService.CreateMeeting(request.Name, request.UserId);

        if (meeting is null)
        {
            throw new ApplicationException($"Error creating meeting: {request.UserId}");
        }
        
        var dbMeeting = new Meeting
        {
            Id = new MeetingId(),
            ZoomMeetingId = meeting.Id,
            Agenda = request.Agenda,
            Name = request.Name,
            Url = meeting.JoinUrl,
            Created = DateTime.Now,
            Scheduled = request.Date,
            CreatorId = creatorId
        };

        await meetingRepository.CreateAsync(dbMeeting, cancellationToken);
    }
}
