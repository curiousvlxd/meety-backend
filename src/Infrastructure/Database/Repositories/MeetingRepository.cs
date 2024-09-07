using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
using Infrastructure.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Database.Repositories;

public sealed class MeetingRepository(IApplicationDbContext context) : IMeetingRepository
{
    public async Task<PagedList<Meeting>> GetAsync(UserId creatorId, Pagination pagination, CancellationToken cancellationToken)
    {
        var query = context.Meetings.Where(x => x.CreatorId == creatorId).AsNoTracking();
        var response = await PagedList<Meeting>.CreateAsync(query, pagination, cancellationToken);
        return response;
    }
    
    public async Task<Meeting> CreateAsync(Meeting meeting, CancellationToken cancellationToken)
    {
        var response = await context.Meetings.AddAsync(meeting, cancellationToken);
        return response.Entity;
    }
    
    public async Task<Meeting> UpdateAsync(Meeting meeting, CancellationToken cancellationToken)
    {
        var response = context.Meetings.Update(meeting);
        return response.Entity;
    }
    
    public async Task<Meeting?> GetByIdAsync(MeetingId id, CancellationToken cancellationToken)
    {
        var response = await context.Meetings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return response;
    }
}
