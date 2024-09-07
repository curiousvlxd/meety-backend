using ZoomNet;
using ZoomNet.Models;
namespace Infrastructure.MeetingService;

public class MeetingService(IZoomClient zoomClient) : IMeetingService
{
    public async Task<Meeting?> CreateMeeting(string name, string stringUserId)
    {
        return await zoomClient.Meetings.CreateInstantMeetingAsync(stringUserId, name, "agenda");
    }
}
