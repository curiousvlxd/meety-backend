using Domain.Entities.Invitation;
using Domain.Entities.Meeting;
using Domain.Entities.User;
using Domain.Primitives;
using Infrastructure.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Database.Repositories;

public sealed class InvitationRepository(IApplicationDbContext context) : IInvitationRepository
{
    public async Task<PagedList<Invitation>> GetAsync(MeetingId meetingId, UserId userId, Pagination pagination, CancellationToken cancellationToken = default)
    {
        var query = context.Invitations
            .Where(x => x.MeetingId == meetingId &&  x.InviteeId == userId)
            .OrderBy(x => x.Created)
            .AsNoTracking();
        var response = await PagedList<Invitation>.CreateAsync(query, pagination, cancellationToken);
        return response;
    }
    
    public async Task<Invitation> CreateAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        var response = await context.Invitations.AddAsync(invitation, cancellationToken);
        return response.Entity;
    }
    
    public async Task<Invitation> UpdateAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        var response = context.Invitations.Update(invitation);
        return response.Entity;
    }
    
    public async Task<Invitation?> Get(UserId inviteeId, MeetingId meetingId, CancellationToken cancellationToken = default)
    {
        var invitation = await context.Invitations
            .FirstOrDefaultAsync(x => x.InviteeId == inviteeId && x.MeetingId == meetingId, cancellationToken);
        return invitation;
    }
}
