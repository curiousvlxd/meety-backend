using Domain.Entities.MessengerProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class MessengerProfileEntityConfiguration: BaseEntityConfiguration<MessengerProfile>
{
    public override void Configure(EntityTypeBuilder<MessengerProfile> builder)
    {
        base.Configure(builder);

        builder.ToTable("MessengerProfiles");

        builder.Property(p => p.Name)
            .HasMaxLength(64);

        builder.Property(p => p.UserName);
        
        builder.HasOne(e => e.User)
            .WithOne(e => e.MessengerProfile)
            .HasForeignKey<MessengerProfile>(e => e.UserId)
            .IsRequired();
    }
}
