using Domain.Entities.Meeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class MeetingEntityConfiguration: BaseEntityConfiguration<Meeting>
{
    public override void Configure(EntityTypeBuilder<Meeting> builder)
    {
        base.Configure(builder);

        builder.ToTable("Meetings");
        
        builder.Property(p => p.Id).HasConversion(
            meetingId => meetingId.Value,
            value => new MeetingId(value));
        
        builder.Property(p => p.Status)
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasMany(e => e.Invitations)
            .WithOne(e => e.Meeting)
            .HasForeignKey(e => e.MeetingId)
            .IsRequired();
    }
}
