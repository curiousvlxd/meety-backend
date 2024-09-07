using Meeting = ZoomNet.Models.Meeting;
namespace Infrastructure.MeetingService;

public interface IMeetingService
{
    Task<Meeting?> CreateMeeting(string name, string stringUserId);
}
