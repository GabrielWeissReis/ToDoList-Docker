using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.EntitiesConfiguration;

namespace Repository.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<ToDoTask> ToDoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ToDoTaskConfiguration());
    }
}
