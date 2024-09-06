using Domain.Entities.Meeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

internal class MeetingConfiguration : EntityConfiguration<Meeting>
{
    public override void Configure(EntityTypeBuilder<Meeting> builder)
    {
        base.Configure(builder);

        builder.ToTable("meetings");
        
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
