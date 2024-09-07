using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Invitation;

public interface IInvitationRepository
{
    Task<PagedList<Invitation>> GetAsync(MeetingId meetingId, UserId userId, Pagination pagination, CancellationToken cancellationToken = default);
    Task<Invitation> CreateAsync(Invitation invitation, CancellationToken cancellationToken = default);
    Task<Invitation> UpdateAsync(Invitation invitation, CancellationToken cancellationToken = default);
    Task<Invitation?> Get(UserId inviteeId, MeetingId meetingId, CancellationToken cancellationToken = default);
}
