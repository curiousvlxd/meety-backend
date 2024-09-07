using Domain.Entities.Meeting;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

internal class MeetingConfiguration : EntityConfiguration<Meeting>
{
    public override void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("meetings");
        
        builder.Property(p => p.Id).HasConversion(
            meetingId => meetingId.Value.ToString(),
            value => new MeetingId(Ulid.Parse(value)));
        
        builder.Property(p => p.CreatorId).HasConversion(
            userId => userId.Value.ToString(),
            value => new UserId(Ulid.Parse(value)));      
        
        builder.HasOne(e => e.Creator)
            .WithMany(e => e.Meetings)
            .HasForeignKey(e => e.CreatorId)
            .IsRequired();
        
        
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
