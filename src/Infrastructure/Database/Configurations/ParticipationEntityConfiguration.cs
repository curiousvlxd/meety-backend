using Domain.Entities.Invitation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class ParticipationEntityConfiguration: BaseEntityConfiguration<Invitation>
{
    public override void Configure(EntityTypeBuilder<Invitation> builder)
    {
        base.Configure(builder);

        builder.ToTable("Meetings");
        
        builder.Property(p => p.Id).HasConversion(
            participationId => participationId.Value,
            value => new InvitationId(value));
        
        builder.HasOne(e => e.Meeting)
            .WithMany(e => e.Invitations)
            .HasForeignKey(e => e.MeetingId)
            .IsRequired();
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.Invitations)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
