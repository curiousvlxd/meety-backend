using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configurations;

public class BaseEntityConfiguration<TEntity>  : IEntityTypeConfiguration<TEntity> 
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(p => p.Created).ValueGeneratedOnAdd();
        builder.Ignore(p => p.DomainEvents);
    }
}
