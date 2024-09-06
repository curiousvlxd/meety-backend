using Domain.Entities.MessengerProfile;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class UserEntityConfiguration: BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(p => p.Email)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasOne(e => e.MessengerProfile)
            .WithOne(e => e.User)
            .HasForeignKey<MessengerProfile>(e => e.UserId)
            .IsRequired();
        
        builder.HasMany(e => e.Participations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.User)
            .IsRequired();
    }
}
