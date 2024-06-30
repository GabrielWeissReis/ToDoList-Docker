using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.EntitiesConfiguration.Base;

namespace Repository.EntitiesConfiguration;

internal class ToDoTaskConfiguration : BaseEntityConfiguration<ToDoTask>
{
    public override void Configure(EntityTypeBuilder<ToDoTask> builder)
    {
        builder.ToTable("ToDoTasks");

        base.Configure(builder);

        builder
            .Property(c => c.Title)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(c => c.Description)
            .HasColumnType("varchar(500)")
            .IsRequired();

        builder
            .Property(c => c.IsCompleted)
            .HasColumnType("bit")
            .IsRequired();
    }
}
