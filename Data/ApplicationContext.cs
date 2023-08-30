using ItExpertTestTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItExpertTestTask.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().ToTable("Items");

        modelBuilder.Entity<Item>()
            .HasData(
                new Item { Id=1,Code=1, Value= "value1" },
                new Item { Id=2,Code=5, Value= "value5" },
                new Item { Id=3,Code=10, Value= "value32" }
            );
    }
}