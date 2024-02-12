using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI.Models;

namespace ShiftLoggerWebAPI;

public class ShiftDbContext : DbContext
{
    
    public ShiftDbContext(DbContextOptions<ShiftDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=workershifts.db;Integrated Security=True");
    }
    
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("Worker");

            entity.HasKey(e => e.WorkerId);
            
            entity.Property(e => e.WorkerId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.HasMany(w => w.Shifts)
                .WithOne(s => s.Worker)
                .HasForeignKey(s => s.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
            
        });
        
        modelBuilder.Entity<Worker>().HasData(
            new Worker { WorkerId = 1, FirstName = "John", LastName = "Doe" },
            new Worker { WorkerId = 2, FirstName = "Jane", LastName = "Smith" },
            new Worker { WorkerId = 3, FirstName = "Michael", LastName = "Johnson" },
            new Worker { WorkerId = 4, FirstName = "Emily", LastName = "Brown" },
            new Worker { WorkerId = 5, FirstName = "Daniel", LastName = "Martinez" },
            new Worker { WorkerId = 6, FirstName = "Olivia", LastName = "Taylor" },
            new Worker { WorkerId = 7, FirstName = "William", LastName = "Anderson" },
            new Worker { WorkerId = 8, FirstName = "Sophia", LastName = "Thomas" },
            new Worker { WorkerId = 9, FirstName = "Matthew", LastName = "Hernandez" },
            new Worker { WorkerId = 10, FirstName = "Ava", LastName = "Walker" },
            new Worker { WorkerId = 11, FirstName = "James", LastName = "Nelson" },
            new Worker { WorkerId = 12, FirstName = "Isabella", LastName = "White" },
            new Worker { WorkerId = 13, FirstName = "Benjamin", LastName = "King" },
            new Worker { WorkerId = 14, FirstName = "Mia", LastName = "Lopez" },
            new Worker { WorkerId = 15, FirstName = "Jacob", LastName = "Gonzalez" },
            new Worker { WorkerId = 16, FirstName = "Charlotte", LastName = "Harris" },
            new Worker { WorkerId = 17, FirstName = "Ethan", LastName = "Clark" },
            new Worker { WorkerId = 18, FirstName = "Amelia", LastName = "Lewis" },
            new Worker { WorkerId = 19, FirstName = "Alexander", LastName = "Lee" },
            new Worker { WorkerId = 20, FirstName = "Harper", LastName = "Robinson" },
            new Worker { WorkerId = 21, FirstName = "Liam", LastName = "Walker" },
            new Worker { WorkerId = 22, FirstName = "Ella", LastName = "Perez" }
        );

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift");

            entity.HasKey(e => e.ShiftId);
            
            entity.Property(e => e.ShiftId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
        });
        
        modelBuilder.Entity<Shift>().HasData(
            new Shift
            {
                ShiftId = 1, StartTime = DateTime.Now.AddHours(-8), EndTime = DateTime.Now.AddHours(-4),
                WorkerId = 1
            },
            new Shift
            {
                ShiftId = 2, StartTime = DateTime.Now.AddHours(-6), EndTime = DateTime.Now.AddHours(-2),
                WorkerId = 2
            },
            new Shift
            {
                ShiftId = 3,
                StartTime = DateTime.Now.AddDays(-2).AddHours(8), // Example start time for shift 3
                EndTime = DateTime.Now.AddDays(-2).AddHours(12), // Example end time for shift 3
                WorkerId = 3
            },
            new Shift
            {
                ShiftId = 4,
                StartTime = DateTime.Now.AddDays(-2).AddHours(10), // Example start time for shift 4
                EndTime = DateTime.Now.AddDays(-2).AddHours(14), // Example end time for shift 4
                WorkerId = 4
            },
            new Shift
            {
                ShiftId = 5,
                StartTime = DateTime.Now.AddDays(-1).AddHours(8),
                EndTime = DateTime.Now.AddDays(-1).AddHours(12),
                WorkerId = 5
            },
            new Shift
            {
                ShiftId = 6,
                StartTime = DateTime.Now.AddDays(-1).AddHours(9),
                EndTime = DateTime.Now.AddDays(-1).AddHours(13),
                WorkerId = 6
            },
            // Add shifts for workers 7 through 22 similarly
            new Shift
            {
                ShiftId = 7,
                StartTime = DateTime.Now.AddDays(-1).AddHours(11),
                EndTime = DateTime.Now.AddDays(-1).AddHours(15),
                WorkerId = 7
            },
            new Shift
            {
                ShiftId = 8,
                StartTime = DateTime.Now.AddDays(-1).AddHours(12),
                EndTime = DateTime.Now.AddDays(-1).AddHours(16),
                WorkerId = 8
            },
            new Shift
            {
                ShiftId = 9,
                StartTime = DateTime.Now.AddDays(-1).AddHours(13),
                EndTime = DateTime.Now.AddDays(-1).AddHours(17),
                WorkerId = 9
            },
            new Shift
            {
                ShiftId = 10,
                StartTime = DateTime.Now.AddDays(-1).AddHours(13),
                EndTime = DateTime.Now.AddDays(-1).AddHours(17),
                WorkerId = 10
            },
            new Shift
            {
                ShiftId = 11,
                StartTime = DateTime.Now.AddDays(-1).AddHours(14),
                EndTime = DateTime.Now.AddDays(-1).AddHours(18),
                WorkerId = 11
            },
            new Shift
            {
                ShiftId = 12,
                StartTime = DateTime.Now.AddDays(-1).AddHours(15),
                EndTime = DateTime.Now.AddDays(-1).AddHours(19),
                WorkerId = 12
            },
            new Shift
            {
                ShiftId = 13,
                StartTime = DateTime.Now.AddDays(-1).AddHours(16),
                EndTime = DateTime.Now.AddDays(-1).AddHours(20),
                WorkerId = 13
            },
            new Shift
            {
                ShiftId = 14,
                StartTime = DateTime.Now.AddDays(-1).AddHours(17),
                EndTime = DateTime.Now.AddDays(-1).AddHours(21),
                WorkerId = 14
            },
            new Shift
            {
                ShiftId = 15,
                StartTime = DateTime.Now.AddDays(-1).AddHours(18),
                EndTime = DateTime.Now.AddDays(-1).AddHours(22),
                WorkerId = 15
            },
            new Shift
            {
                ShiftId = 16,
                StartTime = DateTime.Now.AddDays(-1).AddHours(19),
                EndTime = DateTime.Now.AddDays(-1).AddHours(23),
                WorkerId = 16
            },
            new Shift
            {
                ShiftId = 17,
                StartTime = DateTime.Now.AddDays(-1).AddHours(20),
                EndTime = DateTime.Now.AddDays(-1).AddHours(24),
                WorkerId = 17
            },
            new Shift
            {
                ShiftId = 18,
                StartTime = DateTime.Now.AddDays(-1).AddHours(21),
                EndTime = DateTime.Now.AddDays(-1).AddHours(25),
                WorkerId = 18
            },
            new Shift
            {
                ShiftId = 19,
                StartTime = DateTime.Now.AddDays(-1).AddHours(22),
                EndTime = DateTime.Now.AddDays(-1).AddHours(26),
                WorkerId = 19
            },
            new Shift
            {
                ShiftId = 20,
                StartTime = DateTime.Now.AddDays(-1).AddHours(18),
                EndTime = DateTime.Now.AddDays(-1).AddHours(22),
                WorkerId = 20
            },
            new Shift
            {
                ShiftId = 21,
                StartTime = DateTime.Now.AddDays(-1).AddHours(19),
                EndTime = DateTime.Now.AddDays(-1).AddHours(23),
                WorkerId = 21
            },
            new Shift
            {
                ShiftId = 22,
                StartTime = DateTime.Now.AddDays(-1).AddHours(20),
                EndTime = DateTime.Now.AddDays(-1).AddHours(24),
                WorkerId = 22
            }
        );
        
    }

}