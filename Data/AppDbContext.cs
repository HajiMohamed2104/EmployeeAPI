using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // DbSet for Employees
        public DbSet<Employee> Employees { get; set; } = null!; // Initialize to avoid CS8618 warning

        // Optional: Seed data automatically
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice", Position = "Developer" },
                new Employee { Id = 2, Name = "Bob", Position = "Tester" }
            );
        }
    }
}
