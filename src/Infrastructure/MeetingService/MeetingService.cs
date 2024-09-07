using Infrastructure.Messengers.Telegram.UpdateListener.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
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

