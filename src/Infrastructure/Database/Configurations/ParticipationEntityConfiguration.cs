using Domain.Entities.Participant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class ParticipationEntityConfiguration: BaseEntityConfiguration<Participation>
{
    public override void Configure(EntityTypeBuilder<Participation> builder)
    {
        base.Configure(builder);

        builder.ToTable("Meetings");
        
        builder.HasOne(e => e.Meeting)
            .WithMany(e => e.Participations)
            .HasForeignKey(e => e.MeetingId)
            .IsRequired();
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.Participations)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
