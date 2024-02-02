using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI.Models;

namespace ShiftLoggerWebAPI;

public class ShiftDbContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=workers.db;Integrated Security=True");
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=shifts.db;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("workers");

            entity.HasKey(e => e.WorkerId);

            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.HasMany(w => w.Shifts)
                .WithOne(s => s.Worker)
                .HasForeignKey(s => s.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new Worker { WorkerId = 1, FirstName = "John", LastName = "Doe" },
                new Worker { WorkerId = 2, FirstName = "Jane", LastName = "Smith" }
            );
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("shifts");

            entity.HasKey(e => e.ShiftId);

            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();

            entity.HasData(
                new Shift
                {
                    ShiftId = 1, StartTime = DateTime.Now.AddHours(-8), EndTime = DateTime.Now.AddHours(-4),
                    WorkerId = 1
                },
                new Shift
                {
                    ShiftId = 2, StartTime = DateTime.Now.AddHours(-6), EndTime = DateTime.Now.AddHours(-2),
                    WorkerId = 2
                }
            );
        });
        
        
    }

}