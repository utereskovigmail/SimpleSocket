using Microsoft.EntityFrameworkCore;

namespace SimpleServer;

public class AppDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Заміни на свої актуальні параметри підключення
        optionsBuilder.UseNpgsql("Host=ep-orange-violet-a837i83l-pooler.eastus2.azure.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_iZvCxsgn1Bm8");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().ToTable("messages");
    }
}
