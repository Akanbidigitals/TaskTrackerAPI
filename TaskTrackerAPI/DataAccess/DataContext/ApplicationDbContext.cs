using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Domain.Models;
using Task = TaskTrackerAPI.Domain.Models.Task;


namespace TaskTrackerAPI.DataAccess.DataContext;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Keys
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Task>()
            .HasKey(t => t.Id);
        
        // OnetoMany relationship
        modelBuilder.Entity<Task>()
            .HasOne(t => t.AssignTo)     // Task has one assigned User
            .WithMany(u => u.Tasks)        // User has many Tasks
            .HasForeignKey(t => t.AssignToId) // FK in Task table
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        
        //Seed Users to the database
        var users = new List<User>();
        for (int i = 1; i <= 10; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                UserName = $"User{i}",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Pass{i}@123"), // hashed
                Role = i % 2 == 0 ? Role.Manager : Role.User, // alternate roles
                CreatedAt = DateTime.UtcNow
            });
        }

        modelBuilder.Entity<User>().HasData(users);

    }
}