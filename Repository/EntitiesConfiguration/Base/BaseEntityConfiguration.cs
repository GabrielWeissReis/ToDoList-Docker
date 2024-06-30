using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Base;

namespace Repository.EntitiesConfiguration.Base;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Active).IsRequired();

        builder.Property(c => c.EditedAt).IsRequired(false);
        builder.Property(c => c.DeletedAt).IsRequired(false);
        builder.Property(c => c.CreatedAt).IsRequired();
    }
}
