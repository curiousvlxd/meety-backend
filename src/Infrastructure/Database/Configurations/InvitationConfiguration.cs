using Domain.Entities.Invitation;
using Domain.Entities.Meeting;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

internal class InvitationConfiguration : EntityConfiguration<Invitation>
{
    public override void Configure(EntityTypeBuilder<Invitation> builder)
    {
        base.Configure(builder);

        builder.ToTable("Meetings");

        builder.Property(p => p.Id)
            .HasConversion(participationId => participationId.Value.ToString(),
                value => new InvitationId(Ulid.Parse(value)));
        
        builder.HasOne(e => e.Meeting)
            .WithMany(e => e.Invitations)
            .HasForeignKey(e => e.MeetingId)
            .IsRequired();
        
        builder.HasOne(e => e.Inviter)
            .WithMany(u => u.SentInvitations)
            .HasForeignKey(e => e.InviterId)
            .IsRequired();

        builder.HasOne(e => e.Invitee)
            .WithMany(u => u.ReceivedInvitations) 
            .HasForeignKey(e => e.InviteeId)
            .IsRequired();
        
        builder.Property(p => p.InviteeId)
            .HasConversion(userId => userId.Value.ToString(),
                value => new UserId(Ulid.Parse(value)));
        
        builder.Property(p => p.MeetingId)
            .HasConversion(meetingId => meetingId.Value.ToString(),
                value => new MeetingId(Ulid.Parse(value)));
        
        builder.Property(p => p.InviterId)
            .HasConversion(userId => userId.Value.ToString(),
                value => new UserId(Ulid.Parse(value)));
        
        builder.Property(u => u.Status)
            .HasColumnType("smallint")
            .IsRequired();
    }
}
