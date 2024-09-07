using Domain.Entities.User;
using Domain.Primitives;
namespace Domain.Entities.Meeting;

public interface IMeetingRepository
{
    Task<PagedList<Meeting>> GetAsync(UserId creatorId, Pagination pagination, CancellationToken cancellationToken);
    Task<Meeting> CreateAsync(Meeting meeting, CancellationToken cancellationToken);
    Task<Meeting> UpdateAsync(Meeting meeting, CancellationToken cancellationToken);
    Task<Meeting?> GetByIdAsync(MeetingId id, CancellationToken cancellationToken);
}
