using MediatR;
namespace Api.Apis.Meetings;

public class MeetingServices(
    ILogger<MeetingServices> logger,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator)
{   
    public ILogger<MeetingServices> Logger => logger;
    public IMediator Mediator => mediator;
    public IHttpContextAccessor HttpContextAccessor => httpContextAccessor;
}
