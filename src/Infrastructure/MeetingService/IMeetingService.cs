using ZoomNet.Models;
namespace Infrastructure.MeetingService;

public interface IMeetingService
{
    Task<Meeting?> CreateMeeting(string name, string stringUserId);
}
