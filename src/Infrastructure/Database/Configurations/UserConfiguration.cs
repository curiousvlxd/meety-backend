using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

internal class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.HasMany(e => e.Invitations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.User)
            .IsRequired();
        
        builder.Property(u => u.MessengerType)
            .HasColumnType("smallint")
            .IsRequired();
    }
}
