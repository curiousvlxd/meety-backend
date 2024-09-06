using Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class UserEntityConfiguration: BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.HasMany(e => e.Invitations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.User)
            .IsRequired();
    }
}
