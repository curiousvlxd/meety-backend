using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

internal class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasMany(e => e.ReceivedInvitations)
            .WithOne(e => e.Invitee)
            .HasForeignKey(e => e.InviteeId)
            .IsRequired();
        
        builder.HasMany(e => e.SentInvitations)
            .WithOne(e => e.Inviter)
            .HasForeignKey(e => e.InviterId)
            .IsRequired();
        
        builder.HasMany(e => e.Meetings)
            .WithOne(e => e.Creator)
            .HasForeignKey(e => e.CreatorId)
            .IsRequired();
        
        builder.Property(p => p.Id)
            .HasConversion(userId => userId.Value.ToString(),
                value => new UserId(Ulid.Parse(value)));
        
        builder.Property(p => p.MessengerUserId)
            .HasConversion(messengerUserId => messengerUserId.Value,
                value => new MessengerUserId(value));
        
        builder.Property(p => p.ChatId)
            .HasConversion(chatId => chatId.Value,
                value => new ChatId(value));
        
        builder.Property(u => u.MessengerType)
            .HasColumnType("smallint")
            .IsRequired();
    }
}

